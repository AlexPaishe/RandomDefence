using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTower : MonoCache
{
    public bool go = false;
    [SerializeField] private Transform _point;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _turret;
    [SerializeField] private Laser _gun;
    [SerializeField] private float _radius;
    [SerializeField] private float _damage;
    private Transform _target;
    private int _level = 0;
    public float multiply = 1;


    public override void OnFixedTick()
    {
        if (go == true)
        {
            if (_target != null)
            {
                _gun.FirePoint(true, _target);
                Damage();

            }
            else
            {
                _gun.FirePoint(false, _target);
            }
        }
        else
        {
            _gun.FirePoint(false, _target);
        }
    }

    public override void OnTick()
    {
        if (go == true)
        {
            if (_target == null)
            {
                _turret.localEulerAngles = new Vector3(0, -90, 0);
            }
            if (SearchTarget())
            {
                _turret.LookAt(_target);
            }
        }
    }

    /// <summary>
    /// Реализация поиска врагов
    /// </summary>
    private bool SearchTarget()
    {
        Collider[] targets = Physics.OverlapSphere(_point.position, _radius, _layer);
        if (targets.Length > 0)
        {
            int targetnumber = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (Vector3.Distance(_gun.transform.position, targets[i].transform.position) <= Vector3.Distance(_gun.transform.position, targets[targetnumber].transform.position)
                    && targets[i].GetComponent<EnemyHealth>().Health != 0)
                {
                    targetnumber = i;
                }
            }
            _target = targets[targetnumber].transform;            
            return true;
        }
        _target = null;
        return false;
    }

    /// <summary>
    /// Реализация урона врагу
    /// </summary>
    private void Damage()
    {
        _target.gameObject.GetComponent<EnemyHealth>().Health -= _damage * Time.fixedDeltaTime * multiply;
    }

    /// <summary>
    /// Реализация увиления при контакте с бустером
    /// </summary>
    /// <param name="_boost"></param>
    public void BoosterTime(float _boost)
    {
        _damage *= _boost;
    }

    /// <summary>
    /// Возврат на старые настройки без бустера
    /// </summary>
    public void ClassicTime()
    {
        _damage = Base.EL[_level];
    }

    /// <summary>
    /// Реализация улучшения башни
    /// </summary>
    public void Upgrade()
    {
        _level++;
        if(_level == 3)
        {
            _level = 2;
        }
        _damage = Base.EL[_level];
    }

    /// <summary>
    /// Реализация возврата башни на начальный уровень
    /// </summary>
    public void Downgrade()
    {
        _level = 0;
        _damage = Base.EL[_level];
    }
}
