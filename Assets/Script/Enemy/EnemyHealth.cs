using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoCache
{
    [SerializeField] private float _healthMax;
    [SerializeField] private MeshRenderer[] _mesh;
    [SerializeField] private SkinnedMeshRenderer _skinMesh;
    [SerializeField] private bool _fly;
    [SerializeField] private Animator _anima;
    [SerializeField] private float _speedAnima;
    private float _health = 0;
    private bool _isDeath = false;
    private float _death = 1;
    private EnemyMove _enemyM;
    private EnemySpawn _enemyS;
    private BossSpawn _bossS;
    private EnemyFlyMove _enemyFly;
    private BoxCollider _box;
    private EnemyGiveMoney _money;

    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            if(_health <=0)
            {
                _anima.speed = 0;
                _isDeath = true;
                _box.enabled = false;
                if (_fly == false)
                {
                    _enemyM.SpeedDeath(true);
                }
                else
                {
                    _enemyFly.SpeedDeath(true);
                }
                _money.GiveMoney();
            }
            else if(_health == _healthMax)
            {
                _anima.speed = _speedAnima;
            }
        }
    }

    public float Death
    {
        get
        {
            return _death;
        }

        set
        {
            _death = value;
            if(_death <= 0)
            {
                _isDeath = false;
                _death = 1;
                if (_mesh[0] != null)
                {
                    for (int i = 0; i < _mesh.Length; i++)
                    {
                        _mesh[i].material.SetFloat("_Fade", _death);
                    }
                }
                else
                {
                    _skinMesh.material.SetFloat("_Fade", _death);
                }
                Health = _healthMax;
                if(_fly == false)
                {
                    _enemyM.SpeedDeath(false);
                }
                else
                {
                    _enemyFly.SpeedDeath(false);
                }
                if (_enemyS.boss == false)
                {
                    _enemyS.MonsterWave--;
                    _bossS.Kills++;
                }
                _box.enabled = true;
                if (_fly == false)
                {
                    transform.parent.gameObject.SetActive(false);
                }
                else
                {
                    gameObject.SetActive(false);
                }
            }
        }
    }

    private void Awake()
    {
        _health = _healthMax;
        if(_fly == false)
        {
            _enemyM = transform.parent.gameObject.GetComponent<EnemyMove>();
        }
        else
        {
            _enemyFly = GetComponent<EnemyFlyMove>();
        }
        _enemyS = FindObjectOfType<EnemySpawn>();
        _box = GetComponent<BoxCollider>();
        _money = GetComponent<EnemyGiveMoney>();
        _bossS = FindObjectOfType<BossSpawn>();
    }

    public override void OnTick()
    {
        if(_isDeath == true)
        {
            Death -= Time.deltaTime;
            if (_mesh[0] != null)
            {
                for (int i = 0; i < _mesh.Length; i++)
                {
                    _mesh[i].material.SetFloat("_Fade", Death);
                }
            }
            else
            {
                _skinMesh.material.SetFloat("_Fade", Death);
            }
        }
    }
}
