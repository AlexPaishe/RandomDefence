using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFlyMove : MonoCache
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;
    [SerializeField] private Transform _point;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private EnemyHealth _health;
    private UpgradeSpawn _upgrade;
    private bool _go = false;
    private Vector3 _target;
    private float _speedE;

    private void Awake()
    {
        _upgrade = FindObjectOfType<UpgradeSpawn>();
        _target = _upgrade.transform.position;
        _speedE = _speed;
    }

    public override void OnFixedTick()
    {
        if (_health.Health > 0)
        {
            _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedE * Time.fixedDeltaTime);
            if (_trans.position == _target)
            {
                _health.Health = 0;
            }
            RaycastHit hit;
            if (_go == false)
            {
                if (Physics.Linecast(_trans.position, _point.position, out hit, _layer))
                {
                    _go = true;
                    _target = hit.transform.position;
                }
            }
        }
    }

    /// <summary>
    /// Реализация поворота врага в сторону крепости
    /// </summary>
    public void LookTarget()
    {
        _target = _upgrade.transform.position;
        _target.y = _trans.position.y;
        _go = false;
        _trans.LookAt(_target);
    }

    /// <summary>
    /// Реализация смерти
    /// </summary>
    /// <param name="var"></param>
    public void SpeedDeath(bool var)
    {
        if (var == true)
        {
            _speedE = 0;
        }
        else
        {
            _speedE = _speed;
        }
    }
}
