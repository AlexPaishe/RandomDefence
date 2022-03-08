using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoCache
{
    [SerializeField] private ShildTower _shild;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private float _healthMax;
    [SerializeField] private SphereCollider _col;
    [SerializeField] private BoxCollider _box;
    [SerializeField] private float _rehealthSpeed;
    private int _level = 0;
    private float _health;
    private float _indicator = 0;
    private bool _go = false;
    private float _speed;
    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            if(_health <= 0)
            {
                _go = false;
                _shild.ShildOffset = 0;
                _col.enabled = false;
                _box.enabled = true;
                _indicator = 1;
                _mesh.material.SetFloat("_Damage", _indicator);
            }
            else if(_health> 0 && _health < _healthMax)
            {
                _go = true;
                _indicator = 1 - (_health / _healthMax);
                _mesh.material.SetFloat("_Damage", _indicator);
            }
            else if(_health >= _healthMax)
            {
                _health = _healthMax;
                _go = false;
                _indicator = 0;
                _mesh.material.SetFloat("_Damage", _indicator);
                _col.enabled = true;
                _box.enabled = false;
            }
        }
    }

    private void Awake()
    {
        _health = _healthMax;
        _speed = _rehealthSpeed;
    }

    private void Start()
    {
        _healthMax = Base.ES[0];
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            other.GetComponent<EnemyHealth>().Health = 0;
            Health--;
        }
        else if(other.GetComponent<Eggs>())
        {
            Health = 0;
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// Реализация востановления щита
    /// </summary>
    public void NewLive()
    {
        Health = _healthMax;
    }

    /// <summary>
    /// Возвращение первого цвета щита
    /// </summary>
    public void FirstColorShild()
    {
        _mesh.material.SetFloat("_Damage", 0);
    }

    /// <summary>
    /// Реализация улучшения щита
    /// </summary>
    public void Upgrade()
    {
        _level++;
        if(_level == 3)
        {
            _level = 2;
        }
        _healthMax = Base.ES[_level];
    }

    /// <summary>
    /// Реализация возврата на базовый уровень
    /// </summary>
    public void Downgrade()
    {
        _level = 0;
        _healthMax = Base.ES[_level];
    }

    public override void OnTick()
    {
        if(_go == true)
        {
            Health += Time.deltaTime * _speed;
        }
    }

    /// <summary>
    /// Реализация действия ускорителя
    /// </summary>
    /// <param name="var"></param>
    public void BoosterTime(float var)
    {
        _speed *= 1 + var;
    }

    /// <summary>
    /// Возврат на старые настройки
    /// </summary>
    public void ClassicTime()
    {
        _speed = _rehealthSpeed;
    }
}
