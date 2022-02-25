using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shild : MonoBehaviour
{
    [SerializeField] private ShildTower _shild;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private int _healthMax;
    [SerializeField] private SphereCollider _col;
    [SerializeField] private BoxCollider _box;
    private int _level = 0;
    private float _healthDamage = 0;
    private int _health;
    private float _indicator = 0;
    public int Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            if(_health == 0)
            {
                _shild.ShildOffset = 0;
                _col.enabled = false;
                _box.enabled = true;
            }
            else if(_health == _healthMax)
            {
                _indicator = 0;
                _mesh.material.SetFloat("_Damage", _indicator);
                _col.enabled = true;
                _box.enabled = false;
            }
        }
    }

    private void Awake()
    {
        _healthDamage = 1f / _healthMax;
        _health = _healthMax;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            other.GetComponent<EnemyHealth>().Health = 0;
            Health--;
            _indicator += _healthDamage;
            _mesh.material.SetFloat("_Damage", _indicator);
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
}
