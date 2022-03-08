using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoCache
{
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;
    [SerializeField] private EnemyHealth _eHP;
    private Vector3 _target;
    private float _speedE;
    private int _way = 0;
    private Vector3[] _pointsB = new Vector3[4];
    private Transform[] _point = new Transform[10];
    private float _t = 0;
    public float T
    {
        get
        {
            return _t;
        }

        set
        {
            _t = value;
            if (_t >= 1)
            {
                _t = 0;
                WayS++;
            }
        }
    }

    public int WayS
    {
        get
        {
            return _way;
        }
        set
        {
            _way = value;
            switch (_way)
            {
                case 0: _target = _point[0].position; LookTarget(); T = 0; break;
                case 1: _target = _point[1].position; LookTarget();  T = 0; break;
                case 2: BezierWay(_point[1].position, _point[2].position, _point[3].position, _point[4].position); T = 0; break;
                case 3: BezierWay(_point[4].position, _point[3].position, _point[5].position, _point[6].position); T = 0; break;
                case 4: BezierWay(_point[6].position, _point[5].position, _point[7].position, _point[8].position); T = 0; break;
                case 5: _target = _point[9].position; break;
            }
        }
    }

    private void Awake()
    {
        _speedE = _speed;
    }

    public override void OnFixedTick()
    {
        Way();
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
        if (var == true)
        {
            _speedE = 0;
        }
        else
        {
            _speedE = _speed;
        }
    }

    /// <summary>
    /// Реализация пути
    /// </summary>
    private void Way()
    {
        if (WayS == 0)
        {
            _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedE * Time.fixedDeltaTime * 5);
        }
        else if(WayS == 1 || WayS == 5)
        {
            _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedE * Time.fixedDeltaTime);
        }
        else if (WayS > 1 && WayS < 5)
        {
            _trans.position = Bezier.GetPoint(_pointsB[0], _pointsB[1], _pointsB[2], _pointsB[3], T);
            T += Time.fixedDeltaTime * 0.4f;
        }
        if (_trans.position == _target && WayS == 0 ||
            _trans.position == _target && WayS == 1)
        {
            WayS++;
        }
    }

    /// <summary>
    /// Реализация записи точек безье пути
    /// </summary>
    /// <param name="p0"></param>
    /// <param name="p1"></param>
    /// <param name="p2"></param>
    /// <param name="p3"></param>
    private void BezierWay(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3)
    {
        _pointsB[0] = p0;
        _pointsB[1] = p1;
        _pointsB[2] = p2;
        _pointsB[3] = p3;
    }

    /// <summary>
    /// Реализация записи пути
    /// </summary>
    /// <param name="points"></param>
    public void NewWay(Transform[] points)
    {
        for(int i = 0; i < _point.Length; i++)
        {
            _point[i] = points[i];
        }
        WayS = 0;
        T = 0;
    }

    /// <summary>
    /// Реализация записи пути при рождении из яйца
    /// </summary>
    /// <param name="points"></param>
    public void NewWayEgg(Transform[] points)
    {
        for (int i = 0; i < _point.Length; i++)
        {
            _point[i] = points[i];
        }
        WayS = 1;
        T = 0;
    }

    /// <summary>
    /// Реализация воспроизводства звука при старте
    /// </summary>
    public void EnemyHealthSound()
    {
        _eHP.Begin();
    }

    /// <summary>
    /// Реализация увеличения хп
    /// </summary>
    /// <param name="var"></param>
    public void EnemyHealthUpgrade(float var)
    {
        _eHP.UpgradeHealthMax(var);
    }
}
