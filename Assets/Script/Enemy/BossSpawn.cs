using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private Slider _bossHealthSlider;
    [SerializeField] private float _health;
    [SerializeField] private EnemySpawn _enemySpawn;
    [SerializeField] private Portal[] _portals;
    [SerializeField] private Transform[] _firstTarget;
    [SerializeField] private Transform[] _spawn;
    [SerializeField] private GameObject _boss;
    private GameObject _bossObj;
    private int _count = 0;
    private int _kill = 0;
    public int Kills
    {
        get
        {
            return _kill;
        }
        set
        {
            _kill = value;
            _bossHealthSlider.value = _kill;
            Debug.Log($"{_kill} / {_count}");
            if(_kill == _count)
            {
                _enemySpawn.boss = true;
                Begin();
            }
        }
    }
    private void Awake()
    {
        _count =_enemySpawn.SliderInd();
        _bossHealthSlider.maxValue = _count;
        _bossObj = Instantiate(_boss, _spawn[0].position, Quaternion.identity);
        _bossObj.SetActive(false);
    }

    private void Begin()
    {
        int rand = Random.Range(0, 5);
        for(int i = 0; i < _portals.Length; i++)
        {
            if(i == rand)
            {
                _portals[i].OnPortal();
                _portals[i].ColorPortal(2);
            }
            else
            {
                _portals[i].OffPortal();
            }
        }

        _bossObj.SetActive(true);
        _bossObj.transform.position = _spawn[rand].position;
        BossMove bossMove = _bossObj.GetComponent<BossMove>();
        bossMove.FirstTarget(_firstTarget[rand].position);
        bossMove.go = true;
    }
}
