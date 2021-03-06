using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerSearchEnemy : MonoCache
{
    public bool go = false;
    [SerializeField] private Transform _point;
    [SerializeField] private LayerMask _layer;
    [SerializeField] private Transform _turret;
    [SerializeField] private Shooting[] _gun;
    [SerializeField] private float _timerMax;
    [SerializeField] private float _radius;
    [SerializeField] private SoundTower _sound;
    private float _timer;
    private float _timerM;
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
                _timer = _timerM;
                SpawnBullet();
            }
        }
    }

    private void Awake()
    {
        _timerM = _timerMax;
        _timer = _timerM;
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
                _turret.LookAt(_target);
            }
        }
    }

    /// <summary>
    /// ?????????? ?????? ??????
    /// </summary>
    private bool SearchTarget()
    {
        Collider[] targets = Physics.OverlapSphere(_point.position, _radius, _layer);
        if (targets.Length > 0)
        {
            int targetnumber = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (Vector3.Distance(_gun[_muzzle].transform.position, targets[i].transform.position)
                    <= Vector3.Distance(_gun[_muzzle].transform.position, targets[targetnumber].transform.position)
                    && targets[i].GetComponent<EnemyHealth>() != null)
                {
                    if (targets[i].GetComponent<EnemyHealth>().Health != 0)
                    {
                        targetnumber = i;
                    }
                }
                else if (Vector3.Distance(_gun[_muzzle].transform.position, targets[i].transform.position)
                    <= Vector3.Distance(_gun[_muzzle].transform.position, targets[targetnumber].transform.position)
                    && targets[i].GetComponent<BossHealth>() != null)
                {
                    if (targets[i].GetComponent<BossHealth>().Health != 0)
                    {
                        targetnumber = i;
                    }
                }
            }
            _target = targets[targetnumber].transform;
            return true;
        }
        _target = null;
        return false;
    }

    /// <summary>
    /// ?????????? ???????? ???????
    /// </summary>
    private void SpawnBullet()
    {
        _gun[_muzzle].transform.LookAt(_target);
        _gun[_muzzle].Shoot();
        _sound.Shoot(_muzzle);
        _muzzle++;
        if (_muzzle == _gun.Length)
        {
            _muzzle = 0;
        }
    }

    /// <summary>
    /// ?????????? ????????? ???????? ???????? ??? ????????? ??????????
    /// </summary>
    /// <param name="boost"></param>
    public void BoosterTime(float boost)
    {
        _timerM *= boost;
    }

    /// <summary>
    /// ?????????? ????????? ???????? ?? ?????????
    /// </summary>
    public void ClassicTime()
    {
        _timerM = _timerMax;
    }

    /// <summary>
    /// ?????????? ????????
    /// </summary>
    public void Upgrade()
    {
        for(int i = 0; i < _gun.Length; i++)
        {
            _gun[i].UpgradeShoot();
        }
    }

    /// <summary>
    /// ?????????? ???????? ? ??????? ??????????
    /// </summary>
    public void DownGrade()
    {
        for (int i = 0; i < _gun.Length; i++)
        {
            _gun[i].Downgrade();
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
