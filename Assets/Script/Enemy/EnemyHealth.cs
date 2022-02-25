using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoCache
{
    [SerializeField] private float _healthMax;
    [SerializeField] private MeshRenderer[] _mesh;
    [SerializeField] private bool _fly;
    private float _health = 0;
    private bool _isDeath = false;
    private float _death = 1;
    private EnemyMove _enemyM;
    private EnemySpawn _enemyS;
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
                for(int i = 0; i < _mesh.Length; i++)
                {
                    _mesh[i].material.SetFloat("_Fade", _death);
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
                _enemyS.MonsterWave--;
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
    }

    public override void OnTick()
    {
        if(_isDeath == true)
        {
            Death -= Time.deltaTime;
            for (int i = 0; i < _mesh.Length; i++)
            {
                _mesh[i].material.SetFloat("_Fade", Death);
            }
        }
    }
}
