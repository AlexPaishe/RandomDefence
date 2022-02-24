using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSearchEnemy : MonoCache
{
    public bool go = false;
    [SerializeField] private Transform _point;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _turret;
    [SerializeField] private Transform[] _gun;
    [SerializeField] private bool _flamer;
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _timerMax;
    [SerializeField] private float _radius;
    private float _timer;
    private int _muzzle = 0;
    private Transform _target;

    public float Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
            if(_timer <=0)
            {
                _timer = _timerMax;
                SpawnBullet();
            }
        }
    }

    public float GetMaxTimer()
    {
        return _timerMax;
    }

    private void Awake()
    {
        _timer = _timerMax;
    }

    public override void OnTick()
    {
        if(go == true)
        {
            if(_target == null)
            {
                _turret.localEulerAngles = new Vector3(0, -90, 0);
            }
            if(SearchTarget())
            {
                Timer -= Time.deltaTime;
                if (_flamer == false)
                {
                    _turret.LookAt(_target);
                }
                if (_flamer)
                {
                    _turret.LookAt(_target);
                }
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
            if (_flamer == false)
            {
                int targetnumber = 0;               
                for (int i = 0; i < targets.Length; i++)
                {
                    if (Vector3.Distance(_gun[_muzzle].position, targets[i].transform.position) <= Vector3.Distance(_gun[_muzzle].position, targets[targetnumber].transform.position)
                        && targets[i].GetComponent<EnemyHealth>().Health != 0)
                    {
                        targetnumber = i;
                    }
                }
                _target = targets[targetnumber].transform;
            }
            if (_flamer)
            {
                int targetnumber = 0;
                for (int i = 0; i < targets.Length; i++)
                {
                    if (Vector3.Distance(_gun[_muzzle].position, targets[i].transform.position) <= Vector3.Distance(_gun[_muzzle].position, targets[targetnumber].transform.position)
                        && targets[i].GetComponent<EnemyHealth>().Health != 0)
                    {
                        targetnumber = i;
                    }
                }
                _target = targets[targetnumber].transform;
            }
            return true;
        }
        _target = null;
        return false;
    }

    /// <summary>
    /// Реализация выстрела снаряда
    /// </summary>
    private void SpawnBullet()
    {
        if (_flamer == false)
        {
            GameObject bullet = Instantiate(_bullet, _gun[_muzzle].position, Quaternion.identity);
            bullet.GetComponent<ClassicBullet>().target = _target.position;
            _muzzle++;
            if(_muzzle == _gun.Length)
            {
                _muzzle = 0;
            }
        }
        if (_flamer)
        {           
            GetComponent<Flame>().FlameThrower(Physics.OverlapBox(_gun[_muzzle].position, new Vector3(0.5f, 2f, 5), Quaternion.identity));//Magic num
            
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(_point.position, _radius);
        if(_target != null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(_turret.position, _target.transform.position);
        }
    }
}
