using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoCache
{
    public bool go;
    [SerializeField] private float _speed;
    [SerializeField] private float _speedJump;
    [SerializeField] private Transform _trans;
    [SerializeField] private Vector3 _space;
    [SerializeField] private Transform _behinPointLine;
    [SerializeField] private Transform _endPointLine;
    [SerializeField] private LayerMask _mask;
    [SerializeField] private int _countJump;
    [SerializeField] private Animator _anima;
    [SerializeField] private BossEgg _egg;
     
    private Transform _finish;
    private float _t;
    private Vector3 _target;
    private float _speedBoss;
    private Vector3[] _pointJump = new Vector3[4];
    private bool _jump = false;
    private bool _first = true;
    private int _jumpCount = 0;
    private bool _jumpAnima = false;
    public int JumpCount
    {
        get
        {
            return _jumpCount;
        }
        set
        {
            _jumpCount = value;
            if(_jumpCount > _countJump)
            {
                _jumpCount = 1;
            }
        }
    }

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
                _jumpAnima = false;
                SearchWay();
            }
        }
    }

    private void Awake()
    {
        _speedBoss = _speed;
        _finish = FindObjectOfType<GameOverScript>().gameObject.transform;
    }

    public override void OnTick()
    {
        if(go == true)
        {
            Way();
            if(_jump == true && _jumpAnima == true)
            {
                T += Time.fixedDeltaTime * _speedJump;
            }
        }
    }

    /// <summary>
    /// Реализация движения
    /// </summary>
    private void Way()
    {
        if(_jump == false)
        {
            if (_first == true)
            {
                _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedBoss * Time.deltaTime * 10);
            }
            else
            {
                _trans.position = Vector3.MoveTowards(_trans.position, _target, _speedBoss * Time.deltaTime);
            }
        }
        else
        {
            _trans.position = Bezier.GetPoint(_pointJump[0], _pointJump[1], _pointJump[2], _pointJump[3], T);
            Vector3 pos = _finish.position;
            pos.y = _trans.position.y;
            _trans.LookAt(pos);
        }

        if(_target != null)
        {
            if(_trans.position == _target && _jump == false)
            {
                _first = false;
                SearchWay();
            }
        }
    }

    /// <summary>
    /// Реализация поика пути
    /// </summary>
    private void SearchWay()
    {
        RaycastHit hit;
        if(Physics.Linecast(_behinPointLine.position, _endPointLine.position, out hit, _mask))
        {
            BossPoint bossP = hit.collider.gameObject.GetComponent<BossPoint>();
            if(bossP._pointFirstJump[0] != null && bossP._poitSecondJump[0] != null)
            {
                if (JumpCount != _countJump)
                {
                    JumpCount++;
                    int rand = Random.Range(1, 3);
                    switch (rand)
                    {
                        case 1:
                            _jump = true;
                            for (int i = 0; i < 4; i++)
                            {
                                _pointJump[i] = bossP._pointFirstJump[i].position;
                            }
                            break;
                        case 2:
                            _jump = true;
                            for (int i = 0; i < 4; i++)
                            {
                                _pointJump[i] = bossP._poitSecondJump[i].position;
                            }
                            break;
                    }
                    _egg.Go = true;
                }
                else
                {
                    JumpCount++;
                    _jump = false;
                    _target = bossP.pointNext.position;
                    _anima.SetTrigger("Move");
                }
            }
            else if(bossP._pointFirstJump[0] == null && bossP._poitSecondJump[0] == null)
            {
                _jump = false; _target = bossP.pointNext.position;
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
        Vector3 pos = _finish.position;
        pos.y = _trans.position.y;
        _trans.LookAt(pos);
        _first = true;
        go = true;
    }

    /// <summary>
    /// РЕализация прыжка
    /// </summary>
    public void JumpBegin()
    {
        _jumpAnima = true;
    }
}
