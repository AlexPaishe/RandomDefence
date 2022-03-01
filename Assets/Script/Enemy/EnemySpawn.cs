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
    [SerializeField] private List<Enemy> _enemies;
    [SerializeField] private List<GameObject> _enemiesOnScene;
    [SerializeField] private Transform[] _spawns;
    [SerializeField] private int _enemiesStartWave;
    [SerializeField] private int _newWave;
    [SerializeField] private float _timeSpawn;
    [SerializeField] private float _timeWave;
    [SerializeField] private int _oneBlock;

    [Header("UI")]
    [SerializeField] private Text _textWave;

    private int _wave = 0;
    private int _monsterWave = 0;
    private int[] _mass = new int[6] { 0, 1, 2, 3, 4, 5 };
    private int _enemyWave = 0;
    private int _spawnPointMax = 0;
    private int _spawnPoint = 0;

    private int[] _allType = new int[6] {0,1,2,3,4,5};
    private int _nowType = 0;
    private int _type = 0;
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
            if(_timeWave <= 0)
            {
                _calm = false;
                _wave++;
                _textWave.text = $"Волна {_wave}";
                if (_wave != 1)
                {
                    _enemiesStartWave += _newWave;
                }
                _enemyWave = 0;
                SpawnWave();
                _nowType = (_spawnPointMax + 1) * _oneBlock; 
                _timeWave = _timerW;
                _timeSpawn = _timerS;
                RandomType();
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
                if (_type == _nowType)
                {
                    RandomType();
                    _type = 0;
                }
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

    public override void OnTick()
    {
        if(_calm == true)
        {
            TimeWave -= Time.deltaTime;
        }
        else
        {
            TimeSpawn -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Реализация пересчёта создания точек спавна
    /// </summary>
    private void SpawnWave()
    {
        if(_wave < 6)
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
            _spawnPointMax = 5;
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
                    enemyObj.GetComponent<EnemyMove>().LookTarget();
                }
                else if(enemyObj.GetComponent<EnemyFlyMove>() != null)
                {
                    Vector3 pos = _spawns[_mass[_spawnPoint]].position;
                    pos.y += 2;
                    enemyObj.transform.position = pos;
                    enemyObj.GetComponent<EnemyFlyMove>().LookTarget();
                }
                break;
            }
        }
        _type++;
    }

    /// <summary>
    /// Реализация рандомного типа
    /// </summary>
    private void RandomType()
    {
        for(int i = 0; i < _allType.Length; i++)
        {
            _allType[i] = Random.Range(0, 3);
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
            wa = 15;
        }
        else
        {
            wa = 10;
        }
        int enemyCountBoss = (_enemiesStartWave * 3) + wa;
        return enemyCountBoss;
    }
}
