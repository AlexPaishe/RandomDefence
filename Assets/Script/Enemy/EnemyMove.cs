using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoCache
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;
    private UpgradeSpawn _upgrade;
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
        _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedE * Time.fixedDeltaTime);
    }

    /// <summary>
    /// Реализация поворота врага в сторону крепости
    /// </summary>
    public void LookTarget()
    {
        _trans.LookAt(_target);
    }

    /// <summary>
    /// Реализация смерти
    /// </summary>
    /// <param name="var"></param>
    public void SpeedDeath(bool var)
    {
        if(var == true)
        {
            _speedE = 0;
        }
        else
        {
            _speedE = _speed;
        }
    }
}
