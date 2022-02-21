using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerDefence : MonoCache
{
    [SerializeField] private MeshRenderer[] _meshs;
    [SerializeField] private float _stepH;
    [SerializeField] private float _stepD;
    [SerializeField] private TowerSearchEnemy _tower;
    private bool _isHologramm = false;
    private bool _isDeath = false;
    private float _hologram;
    private float _death;

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
                _isHologramm = false;
            }
            else if(_hologram == 0)
            {
                if (_tower != null)
                {
                    _tower.go = false;
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
                transform.parent.parent.GetComponent<Cell>().Select = false;
                gameObject.SetActive(false);
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
    }
}
