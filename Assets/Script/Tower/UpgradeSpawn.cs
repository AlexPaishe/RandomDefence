using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeSpawn : MonoBehaviour
{
    [SerializeField] private GameObject _upgrade;
    [SerializeField] private List<GameObject> _upgradeOnScene;
    [SerializeField] private List<Vector3> _upgradeSpawn;
    [SerializeField] private Transform[] _transPoint;
    [SerializeField] private float _transUp;
    private int _count;

    private void Awake()
    {
        _count = _transPoint.Length;

        for(int i = 0; i < _count; i++)
        {
            Vector3 pos = _transPoint[i].position;
            pos.y += _transUp;
            _upgradeSpawn.Add(pos);
            GameObject upgrade = Instantiate(_upgrade, pos, Quaternion.identity);
            _upgradeOnScene.Add(upgrade);
            upgrade.SetActive(false);
        }
    }

    /// <summary>
    /// Реализация спавна апгрейдов
    /// </summary>
    public void SpawnUpgrade()
    {
        int random = Random.Range(0, _count);
        for(int i = 0; i < _count; i++)
        {
            if(_upgradeOnScene[i].activeSelf == false)
            {
                _upgradeOnScene[i].SetActive(true);
                GameObject upgrade = _upgradeOnScene[i];
                upgrade.transform.position = _upgradeSpawn[random];
                upgrade.GetComponent<UpgradeMove>().Target = _transPoint[random].position;
                break;
            }
        }
    }
}
