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

    private Vector3[] _pointsWay = new Vector3[2];
    private bool _go = false;
    private Vector3 _target;
    private float _speedE;
    private int _way = 0;
    private bool _egg = false;
    private GameOverScript _gameOver;
    public int WayS
    {
        get
        {
            return _way;
        }

        set
        {
            _way = value;
            if(_way == 0 && _egg == false)
            {
                _target = _pointsWay[0];
                LookTarget();
            }
            else if(_way == 0 && _egg == true)
            {
                Vector3 pos = _trans.position;
                pos.y += 2;
                _target = pos;
                LookTargetEgg();
            }
            else if(_way == 1)
            {
                _target = _pointsWay[1];
                LookTarget();
            }
        }
    }

    private void Awake()
    {
        _speedE = _speed;
        _gameOver = FindObjectOfType<GameOverScript>();
    }

    public override void OnFixedTick()
    {
        if (_health.Health > 0)
        {
            if (WayS == 0)
            {
                _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedE * Time.fixedDeltaTime * 5);
            }
            else
            {
                _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedE * Time.fixedDeltaTime);
            }
        
            if (_trans.position == _target && _target == _pointsWay[1])
            {
                _health.Health = 0;
            }
            else if (_trans.position == _target && _target == _pointsWay[0] && _egg == false ||
                     _trans.position == _target && _egg == true)
            {
                WayS++;
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
        Vector3 pos = _target;
        pos.y = _trans.position.y;
        _go = false;
        _trans.LookAt(pos);
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
            _egg = false;
        }
        else
        {
            _speedE = _speed;
        }
    }

    /// <summary>
    /// Реализация записи пути
    /// </summary>
    /// <param name="points"></param>
    public void NewWay(Transform[] points)
    {
        for(int i = 0; i < _pointsWay.Length; i++)
        {
            _pointsWay[i] = points[i].position;
        }
        WayS = 0;
    }

    public void NewWayEgg(Transform[] points)
    {
        for (int i = 0; i < _pointsWay.Length; i++)
        {
            _pointsWay[i] = points[i].position;
        }
        _egg = true;
        WayS = 0;
    }

    /// <summary>
    /// Реализация поворота врага в сторону крепости
    /// </summary>
    private void LookTargetEgg()
    {
        Vector3 pos = _gameOver.gameObject.transform.position;
        pos.y = _trans.position.y;
        _go = false;
        _trans.LookAt(pos);
    }
}
