using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefence : MonoCache
{
    public int Type;
    [SerializeField] private MeshRenderer[] _meshs;
    [SerializeField] private float _stepH;
    [SerializeField] private float _stepD;
    [SerializeField] private TowerSearchEnemy _tower;
    [SerializeField] private ShildTower _shild;
    [SerializeField] private SpeedBoosterTower _booster;
    [SerializeField] private Flamer _flamer;
    [SerializeField] private LaserTower _laser;
    [SerializeField] private BoxCollider _lastShot;
    [SerializeField] private SoundTower _soundTower;
    private bool _isHologramm = false;
    private bool _isDeath = false;
    private float _hologram;
    private float _death;
    private BigMoneySystem _bigMoney;

    public float Hologramm
    {
        get
        {
            return _hologram;
        }

        set
        {
            _hologram = value;
            if(_hologram >= 1)
            {
                if (_tower != null)
                {
                    _tower.go = true;
                }
                else if(_flamer != null)
                {
                    _flamer.go = true;
                }
                else if (_laser != null)
                {
                    _laser.go = true;
                }
                _isHologramm = false;
            }
            else if(_hologram == 0)
            {
                if (_tower != null)
                {
                    _tower.go = false;
                }
                else if(_flamer != null)
                {
                    _flamer.go = false;
                }
                else if(_laser != null)
                {
                    _laser.go = false;
                }

                _isHologramm = true;
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
            if(_death >= 1)
            {
                Hologramm = 0;
                _death = 0;
                for (int i = 0; i < _meshs.Length; i++)
                {
                    _meshs[i].material.SetFloat("_Death", _death);
                }
                _isDeath = false;
                transform.parent.GetComponent<Cell>().Select = false;
                _bigMoney.infoState[5]++;
                gameObject.SetActive(false);
            }
            else if(_death > 0 && _death < 0.1f)
            {
                _soundTower.Explosion();
            }
            else if (_death > 0.1f && _death < 0.5f)
            {
                _lastShot.enabled = true;
                if(_tower != null)
                {
                    _tower.go = false;
                    _tower.DownGrade();
                }
                else if(_shild != null)
                {
                    _shild.DownGrade();
                }
                else if(_booster != null)
                {
                    _booster.Downgrade();
                }
                else if(_flamer != null)
                {
                    _flamer.go = false;
                    _flamer.Downgrade();
                }
                else if(_laser != null)
                {
                    _laser.go = false;
                    _laser.Downgrade();
                }
            }
            else if(_death > 0.5f || _death == 0)
            {
                _lastShot.enabled = false;
            }            
        }
    }

    public override void OnTick()
    {
        if(_isHologramm == true)
        {
            Hologramm += _stepH * Time.deltaTime;
            for(int i = 0; i < _meshs.Length; i++)
            {
                _meshs[i].material.SetFloat("_Edge", Hologramm);
            }
        }

        if(_isDeath == true)
        {
            Death += _stepD * Time.deltaTime;
            for (int i = 0; i < _meshs.Length; i++)
            {
                _meshs[i].material.SetFloat("_Death", Death);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            EnemyHealth HP = other.GetComponent<EnemyHealth>();
            if (HP.Health > 0)
            {
                _isDeath = true;
            }
            HP.Health = 0;
        }
        else if(other.GetComponent<Eggs>())
        {
            _isDeath = true;
            Destroy(other.gameObject);
        }
        else if(other.GetComponent<BossHealth>())
        {
            BossHealth hp = other.GetComponent<BossHealth>();
            if(hp.Health > 0)
            {
                _isDeath = true;
            }
            hp.Health -= 50;
        }
    }

    private void Awake()
    {
        _bigMoney = FindObjectOfType<BigMoneySystem>();
    }
}
