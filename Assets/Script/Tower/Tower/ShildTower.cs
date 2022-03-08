using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShildTower : MonoCache
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Shild _shild;
    [SerializeField] private float _step;
    private float _shildOffset;
    private bool _go = false;
    public float multiply = 1;
    public float ShildOffset
    {
        get
        {
            return _shildOffset;
        }

        set
        {
            _shildOffset = value;
            if(_shildOffset >= 1)
            {
                _go = true;
                _shild.NewLive();
            }
            else if(_shildOffset == 0)
            {
                _go = false;
            }
        }
    }

    public override void OnTick()
    {
        if(_go == false)
        {
            ShildOffset += _step * Time.deltaTime * multiply;
            _mesh.material.SetFloat("_Edge", ShildOffset);
        }
    }

    /// <summary>
    /// Реализация появление щита
    /// </summary>
    public void NewLive()
    {
        ShildOffset = 0;
        _shild.FirstColorShild();
    }

    /// <summary>
    /// Реализация улучшения щита
    /// </summary>
    public void Upgrade()
    {
        _shild.Upgrade();
    }

    /// <summary>
    /// Реализация возврата на базовые настройки
    /// </summary>
    public void DownGrade()
    {
        _shild.Downgrade();
    }
}
