using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoCache
{
    public bool go;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;
    [SerializeField] private Vector3 _space;
    [SerializeField] private Transform _finish;
    
    private float _t;
    private Vector3 _target;
    private Vector3 _targetSecond;
    private float _speedBoss;
    private Vector3[] _pointJump = new Vector3[4];
    private Vector3[] _pointJumpSecond = new Vector3[4];
    private bool _jump = false;

    public float T
    {
        get
        {
            return _t;
        }

        set
        {
            _t = value;
            if(_t >= 1)
            {
                _t = 0;
                if(_target != null && _target != _space)
                {
                    _jump = false;
                }
                else
                {
                    for(int i = 0; i < _pointJump.Length;i++)
                    {
                        _pointJump[i] = _pointJumpSecond[i];
                    }
                }
            }
        }
    }

    private void Awake()
    {
        _speedBoss = _speed;
    }

    public override void OnTick()
    {
        if(go == true)
        {
            Way();
        }
    }

    private void Way()
    {
        if(_jump == false)
        {
            _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedBoss * Time.deltaTime);
        }
        else
        {
            _trans.position = Bezier.GetPoint(_pointJump[0], _pointJump[1], _pointJump[2], _pointJump[3], T);

            T += Time.fixedDeltaTime * 0.4f;
            Vector3 pos = _finish.position;
            pos.y = _trans.position.y;
            _trans.LookAt(pos);
        }

        if(_target != null)
        {
            if(_trans.position == _target)
            {
                if(_targetSecond != null && _targetSecond != _space)
                {
                    _target = _targetSecond;
                    _trans.LookAt(_target);
                }
                else
                {
                    _jump = true;
                    _target = _space;
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<BossPoint>())
        {
            int rand = Random.Range(0, 3);
            BossPoint bossP = GetComponent<BossPoint>();
            switch(rand)
            {
                case 0: 
                    if(_target != null && _target != _space)
                    {
                        _targetSecond = bossP.pointNext.position;
                    }
                    else if(_target == null || _target == _space)
                    {
                        _target = bossP.pointNext.position;
                    }
                    break;
                case 1:
                    if(_target != null && _target != _space)
                    {
                        for(int i = 0; i < _pointJump.Length; i ++)
                        {
                            _pointJump[i] = bossP._pointFirstJump[i].position;
                        }
                    }
                    else 
                    {
                        for(int i = 0; i < _pointJumpSecond.Length; i++)
                        {
                            _pointJumpSecond[i] = bossP._pointFirstJump[i].position;
                        }
                    }
                    break;
                case 2:
                    if (_target != null && _target != _space)
                    {
                        for (int i = 0; i < _pointJump.Length; i++)
                        {
                            _pointJump[i] = bossP._poitSecondJump[i].position;
                        }
                    }
                    else
                    {
                        for (int i = 0; i < _pointJumpSecond.Length; i++)
                        {
                            _pointJumpSecond[i] = bossP._poitSecondJump[i].position;
                        }
                    }
                    break;

            }
        }
    }

    /// <summary>
    /// Реализация первого выхода
    /// </summary>
    /// <param name="target"></param>
    public void FirstTarget( Vector3 target)
    {
        _target = target;
        _jump = false;
        _trans.LookAt(_target);
    }
}
