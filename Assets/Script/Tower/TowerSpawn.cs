using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    private int _allType;
    private int _count = 0;

    private void Awake()
    {
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
        int random = Random.Range(0, _allType);
        random *= _count;
        for(int i = 0; i < _towersOnScene.Count; i++)
        {
            if(i >= random && i < random + _count && _towersOnScene[i].activeSelf == false)
            {
                _towersOnScene[i].SetActive(true);
                GameObject tower = _towersOnScene[i];
                tower.transform.position = trans.position;
                tower.transform.SetParent(trans);
                tower.transform.localEulerAngles = new Vector3(0,180,0);
                TowerDefence towerD = tower.GetComponent<TowerDefence>();
                towerD.Hologramm = 0;
                towerD.Death = 0;
                break;
            }
        }
    }
}
