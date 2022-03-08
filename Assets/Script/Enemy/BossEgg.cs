using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossEgg : MonoCache
{
    [SerializeField] private BossMove _boss;
    [SerializeField] private Transform[] _pointFirst;
    [SerializeField] private Transform _pointNext;
    [SerializeField] private GameObject _egg;
    [SerializeField] private float _timerJ;
    [SerializeField] private Animator _anima;
    [SerializeField] private int _eggCount;
    [SerializeField] private EnemySound _sound;
    private EggSystem _eggSystem;
    private Vector3[] _nextPoint = new Vector3[2];
    private float _timer;
    private bool _go = false;
    public float Timer
    {
        get
        {
            return _timer;
        }

        set
        {
            _timer = value;
            if(_timer <= 0)
            {
                _timer = _timerJ;
                Go = false;
                _anima.SetTrigger("Jump");
                JumpNow();
            }
        }
    }

    public bool Go
    {
        get
        {
            return _go;
        }
        set
        {
            _go = value;
            if(_go == true)
            {
                EggSpawn();
            }
        }
    }

    private void Awake()
    {
        _eggSystem = FindObjectOfType<EggSystem>();
        _timer = _timerJ;
    }

    /// <summary>
    /// Реализация прыжка
    /// </summary>
    public void JumpNow()
    {
        _boss.JumpBegin();
    }

    /// <summary>
    /// Реализация создания яйца
    /// </summary>
    public void EggSpawn()
    {
        for(int i = 0; i < _eggCount; i++)
        {
            GameObject egg = Instantiate(_egg, _pointFirst[i].position, Quaternion.identity);
            Eggs eggs = egg.GetComponent<Eggs>();
            int rand = Random.Range(0, 101);
            if (rand < 25)
            {
                _nextPoint = _eggSystem.EggPoint(false);
                eggs.line = _eggSystem.Line;
                eggs.TypeEgg(0);
            }
            else
            {
                _nextPoint = _eggSystem.EggPoint(true);
                int random = Random.Range(0, 101);
                if (rand > 30)
                {
                    random = 1;
                    eggs.line = _eggSystem.Line;
                    eggs.TypeEgg(random);
                }
                else
                {
                    random = 2;
                    eggs.line = _eggSystem.Line;
                    eggs.TypeEgg(random);
                }
            }
            eggs.point[0] = _pointFirst[i].position;
            eggs.point[1] = _pointNext.position;
            eggs.point[2] = _nextPoint[0];
            eggs.point[3] = _nextPoint[1];
        }
        _sound.Begin();
    }

    public override void OnTick()
    {
        if(Go == true)
        {
            Timer -= Time.deltaTime;
        }
    }
}
