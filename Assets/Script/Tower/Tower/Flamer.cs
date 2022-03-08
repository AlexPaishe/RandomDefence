using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flamer : MonoCache
{
    public bool go = false;
    [SerializeField] private float _timerMax;
    [SerializeField] private float _radius;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _point;
    [SerializeField] private Transform _gun;
    [SerializeField] private float _speedReboot;
    [SerializeField] private float _damage;
    [SerializeField] private Vector3 _halfOverBox;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private GameObject _flamerPS;
    [SerializeField] private SoundTower _sound;
    public float multiply = 1;
    private float _timerM;
    private float _timer;
    private bool _flame = true;
    private float _speed;
    private int _level;
    private bool _go = false;

    public bool Go
    {
        get
        {
            return _go;
        }
        set
        {
            _go = value;
            if(_go == true)
            {
                _sound.Shoot(0);
            }
            else
            {
                _sound.StopShoot();
            }
        }
    }
    public float Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
            float flamerOil = _timer / _timerM;
            _mesh.material.SetFloat("_EdgeFlame", flamerOil);
            if(_timer <= 0)
            {
                _flame = false;
            }
            else if(_timer >= _timerM)
            {
                _flame = true;
            }
        }
    }

    private void Awake()
    {
        _timerM = _timerMax;
        Timer = _timerM;
        _speed = _speedReboot;
    }

    private void Start()
    {
        _damage = Base.CF[0];
    }

    public override void OnTick()
    {
        if(go == true)
        {
            if(SearchTarget())
            {
                if (_flame == true)
                {
                    Timer -= Time.deltaTime;
                }
                else
                {
                    Timer += Time.deltaTime * _speed;
                }
            }
            else
            {
                if(Timer < _timerM)
                {
                    Timer += Time.deltaTime * _speed;
                }
            }
        }
    }

    public override void OnFixedTick()
    {
        if(_flame == true && go == true && SearchTarget() == true)
        {
            if(Go == false)
            {
                Go = true;
            }
            FlameFire();
        }
        else
        {
            if(Go == true)
            {
                Go = false;
            }
        }
    }

    /// <summary>
    /// Реализация поиска врагов
    /// </summary>
    /// <returns></returns>
    private bool SearchTarget()
    {
        Collider[] targets = Physics.OverlapBox(_point.position, _halfOverBox, transform.rotation, _layer);
        if(targets.Length > 0)
        {
            return true;
        }
        return false;
    }

    /// <summary>
    /// Реализация поджарирования врагов
    /// </summary>
    /// <returns></returns>
    private void FlameFire()
    {
        Collider[] targets = Physics.OverlapBox(_point.position, _halfOverBox, transform.rotation, _layer);
        for(int i = 0; i < targets.Length; i++)
        {
            if (targets[i].GetComponent<EnemyHealth>() != null)
            {
                targets[i].GetComponent<EnemyHealth>().Health -= _damage * Time.fixedDeltaTime * multiply;
            }
            else if(targets[i].GetComponent<BossHealth>() != null)
            {
                targets[i].GetComponent<BossHealth>().Health -= _damage * Time.fixedDeltaTime * multiply;
            }
        }
        Instantiate(_flamerPS, _gun.position, _gun.rotation);
    }

    /// <summary>
    /// Реализация апгрейда
    /// </summary>
    public void Upgrade()
    {
        _level++;
        if(_level == 3)
        {
            _level = 2;
        }
        _damage = Base.CF[_level];
    }

    /// <summary>
    /// Реализация возвращения к базовым настройкам
    /// </summary>
    public void Downgrade()
    {
        _level = 0;
        _damage = Base.CF[_level];
    }

    /// <summary>
    /// Реализация ускорения горения
    /// </summary>
    /// <param name="boost"></param>
    public void BoostTimer(float boost)
    {
        _speed = _speedReboot * ((1 - boost) + 1);
    }

    /// <summary>
    /// Реализация возврата на базовое время горения
    /// </summary>
    public void ClassicTimer()
    {
        _speed = _speedReboot;
    }
}
