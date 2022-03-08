using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class Tower
{
    public GameObject prefabs;
    public int count;
}

public class TowerSpawn : MonoBehaviour
{
    [SerializeField] private List<Tower> _towers;
    [SerializeField] private List<GameObject> _towersOnScene;
    [SerializeField] private Transform _trans;
    [SerializeField] private Image[] _icon;
    [SerializeField] private Sprite[] _iconSprite;
    [Header("Deck")]
    [SerializeField] private GameObject[] _towerCard;
    [SerializeField] private Sprite[] _iconCard;
    private int _allType;
    private int _count = 0;
    private int _typeTower;
    private Sprite[] _is = new Sprite[5];
    public int TypeTower
    {
        get
        {
            return _typeTower;
        }

        set
        {
            _typeTower = value;
            if (_typeTower == _allType)
            {
                _typeTower = 0;
            }
            for (int i = 0; i < _iconSprite.Length; i++)
            {
                if (i != _iconSprite.Length - 1)
                {
                    _is[i] = _iconSprite[i + 1];
                }
                else
                {
                    _is[i] = _iconSprite[0];
                }
            }

            for (int i = 0; i < _iconSprite.Length; i++)
            {
                _iconSprite[i] = _is[i];
                _icon[i].sprite = _iconSprite[i];
            }
        }
    }

    private void Awake()
    {
        SearchDeck();
        for(int i = 0; i < _towers.Count; i++)
        {
            for(int j = 0; j < _towers[i].count; j++)
            {
                GameObject towerObject = Instantiate(_towers[i].prefabs, _trans.position, Quaternion.identity);
                _towersOnScene.Add(towerObject);
                towerObject.SetActive(false);
            }
        }
        _allType = _towers.Count;
        _count = _towers[0].count;
    }

    /// <summary>
    /// Реализация спавна башен
    /// </summary>
    /// <param name="trans"></param>
    public void BuildTower(Transform trans)
    {
        int random = _typeTower;
        random *= _count;
        for(int i = 0; i < _towersOnScene.Count; i++)
        {
            if(i >= random && i < random + _count && _towersOnScene[i].activeSelf == false)
            {
                _towersOnScene[i].SetActive(true);
                GameObject tower = _towersOnScene[i];
                tower.transform.position = trans.position;
                tower.transform.SetParent(trans);
                tower.transform.localEulerAngles = new Vector3(0,-90,0);
                TowerDefence towerD = tower.GetComponent<TowerDefence>();
                towerD.Hologramm = 0;
                towerD.Death = 0;
                if(tower.GetComponent<ShildTower>() != null)
                {
                    tower.GetComponent<ShildTower>().NewLive();
                }
                else if(tower.GetComponent<SpeedBoosterTower>() != null)
                {
                    tower.GetComponent<SpeedBoosterTower>().BoostZone();
                }
                break;
            }
        }
        TypeTower++;
    }

    /// <summary>
    /// Реализация поиска колоды
    /// </summary>
    public void SearchDeck()
    {
        for (int i = 0; i < _towers.Count; i++)
        {
            int card = PlayerPrefs.GetInt($"Tower{i}");
            _towers[i].prefabs = _towerCard[card];
            _iconSprite[i] = _iconCard[card];
            _icon[i].sprite = _iconSprite[i];
        }
    }
}
