using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
class Enemy
{
    public GameObject prefabs;
    public int count;
}

public class EnemySpawn : MonoCache
{
    public bool boss = false;
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemiesOnScene;
    [SerializeField] private Transform[] _spawns;
    [SerializeField] private int _enemiesStartWave;
    [SerializeField] private int _newWave;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private float _timeWave;
    [SerializeField] private Portal[] _portals;
    [SerializeField] private EnemyMoveSystem _enemySystem;

    [Header("UI")]
    [SerializeField] private Text _textWave;

    private int _wave = 0;
    private int _monsterWave = 0;
    private int[] _mass = new int[5] { 0, 1, 2, 3, 4 };
    private int _enemyWave = 0;
    private int _spawnPointMax = 0;
    private int _spawnPoint = 0;

    private int[] _allType = new int[5] {0,1,2,3,4};
    private int _count = 0;

    private float _timerW = 0;
    private float _timerS = 0;

    private bool _calm = true;

    public float TimeWave
    {
        get
        {
            return _timeWave;
        }

        set
        {
            _timeWave = value;
            if(_timeWave == _timerW)
            {
                _wave++;
                SpawnWave();
                RandomType();
            }
            else if(_timeWave <= 0)
            {
                _calm = false;
                _textWave.text = $"{_wave}";
                if (_wave != 1)
                {
                    _enemiesStartWave += _newWave;
                }
                _enemyWave = 0;
                _timeSpawn = _timerS;
            }
        }
    }

    public float TimeSpawn

    {
        get
        {
            return _timeSpawn;
        }

        set
        {
            _timeSpawn = value;
            if(_timeSpawn <= 0 && _enemyWave != _enemiesStartWave)
            {
                SpawnEnemies();
                _timeSpawn = _timerS;
            }
        }
    }

    public int MonsterWave
    {
        get
        {
            return _monsterWave;
        }

        set
        {
            _monsterWave = value;
            if(_monsterWave == 0)
            {
                _calm = true;
                TimeWave = _timerW;
            }
        }
    }

    private void Awake()
    {
        for(int i = 0; i < _enemies.Count; i++)
        {
            for(int j = 0; j < _enemies[i].count; j++)
            {
                GameObject enemyObj = Instantiate(_enemies[i].prefabs, _spawns[0].position, Quaternion.identity);
                _enemiesOnScene.Add(enemyObj);
                enemyObj.SetActive(false);
            }
        }

        _timerW = _timeWave;
        _timerS = _timeSpawn;
        _count = _enemies[0].count;
    }

    private void Start()
    {
        TimeWave = _timerW;
    }

    public override void OnTick()
    {
        if (boss == false)
        {
            if (_calm == true)
            {
                TimeWave -= Time.deltaTime;
            }
            else
            {
                TimeSpawn -= Time.deltaTime;
            }
        }
    }

    /// <summary>
    /// Реализация пересчёта создания точек спавна
    /// </summary>
    private void SpawnWave()
    {
        if(_wave < _mass.Length)
        {
            for(int i = _mass.Length - 1; i >= 0; i--)
            {
                int j = Random.Range(0, i);
                int tmp = _mass[i];
                _mass[i] = _mass[j];
                _mass[j] = tmp;
            }
            _spawnPointMax = _wave - 1;
        }
        else
        {
            _spawnPointMax = 4;
        }
    }

    /// <summary>
    /// Спавн одного монстра
    /// </summary>
    private void SpawnEnemies()
    {
        MonsterWave++;
        _enemyWave++;
        _spawnPoint++;
        if (_spawnPoint > _spawnPointMax)
        {
            _spawnPoint = 0;
        }

        int type = _allType[_spawnPoint] * _count;
        for (int i = 0; i < _enemiesOnScene.Count; i++)
        {
            if(_enemiesOnScene[i].activeSelf == false && i >= type && i < type + _count)
            {
                _enemiesOnScene[i].SetActive(true);
                GameObject enemyObj = _enemiesOnScene[i];
                if (enemyObj.GetComponent<EnemyMove>() != null)
                {
                    enemyObj.transform.position = _spawns[_mass[_spawnPoint]].position;
                    enemyObj.GetComponent<EnemyMove>().NewWay(_enemySystem.EnemyWayPoint(_mass[_spawnPoint]));
                }
                else if(enemyObj.GetComponent<EnemyFlyMove>() != null)
                {
                    enemyObj.transform.position = _spawns[_mass[_spawnPoint]].position;
                    enemyObj.GetComponent<EnemyFlyMove>().NewWay(_enemySystem.EnemyWayPoint(_mass[_spawnPoint] + 5));
                }
                break;
            }
        }
    }

    /// <summary>
    /// Реализация рандомного типа
    /// </summary>
    private void RandomType()
    {
        for(int i = 0; i < _allType.Length; i++)
        {
            _allType[i] = Random.Range(0, 3);
            if(_allType[i] == 0)
            {
                _portals[_mass[i]].ColorPortal(1);
            }
            else
            {
                _portals[_mass[i]].ColorPortal(0);
            }
        }
        for(int i = 0; i < _mass.Length; i++)
        {
            if(i <= _spawnPointMax)
            {
                _portals[_mass[i]].OnPortal();
            }
            else
            {
                _portals[_mass[i]].OffPortal();
            }
        }
    }

    /// <summary>
    /// Реализация показала сколько монстров осталось
    /// </summary>
    public int SliderInd()
    {
        int wa = 0;
        if(_wave != 0)
        {
            wa = 150;
        }
        else
        {
            wa = 100;
        }
        int enemyCountBoss = (_enemiesStartWave * 5) + wa;
        return enemyCountBoss;
    }
}
