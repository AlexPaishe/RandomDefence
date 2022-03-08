using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossHealth : MonoCache
{
    [SerializeField] private SkinnedMeshRenderer[] _mesh;
    [SerializeField] private Animator _anima;
    [SerializeField] private BoxCollider _box;
    [SerializeField] private EnemyGiveMoney _money;
    private float _health = 0;
    private bool _isDeath = false;
    private float _death = 1;
    private EnemySpawn _enemyS;
    private BossSpawn _bossS;
    public float Health
    {
        get
        {
            return _health;
        }

        set
        {
            _health = value;
            _bossS.BossHealthSlider(_health);
            if(_health <= 0)
            {
                _isDeath = true;
                _anima.speed = 0;
                _box.enabled = false;
                _money.GiveMoney();
                EnemyHealth[] enemy = FindObjectsOfType<EnemyHealth>();
                for(int i = 0; i < enemy.Length; i++)
                {
                    if (enemy[i].gameObject.activeSelf)
                    {
                        enemy[i].Health = 0;
                    }
                }
                Portal[] portal = FindObjectsOfType<Portal>();
                for(int i = 0; i < portal.Length; i++)
                {
                    portal[i].OnPortal();
                }
                Eggs[] egg = FindObjectsOfType<Eggs>();
                for(int i = 0; i < egg.Length; i++)
                {
                    Destroy(egg[i].gameObject);
                }
                _bossS.BossWave();
            }
        }
    }

    public float Death
    {
        get
        {
            return _death;
        }

        set
        {
            _death = value;
            if(_death <= 0)
            {
                _death = 1;
                _isDeath = false;
                _box.enabled = true;
                _anima.speed = 1;
                _enemyS.boss = false;
                for (int i = 0; i < _mesh.Length; i++)
                {
                    _mesh[i].material.SetFloat("_Fade", _death);
                }
                gameObject.SetActive(false);
            }
        }
    }

    public override void OnTick()
    {
        if(_isDeath == true)
        {
            Death -= Time.deltaTime * 0.5f;
            for(int i = 0; i < _mesh.Length; i++)
            {
                _mesh[i].material.SetFloat("_Fade", Death);
            }
        }
    }

    private void Awake()
    {
        _enemyS = FindObjectOfType<EnemySpawn>();
        _bossS = FindObjectOfType<BossSpawn>();
    }

    /// <summary>
    /// Ќазначение награды за убийство босса
    /// </summary>
    /// <param name="var"></param>
    public void NewPrice(int var)
    {
        _money.priceKill = var;
    }
}
