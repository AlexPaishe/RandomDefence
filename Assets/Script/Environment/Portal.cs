using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal : MonoCache
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Vector3[] _portalSize;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;
    private Vector3 _target;

    public override void OnTick()
    {
        if (_trans.localScale != _target)
        {
            _trans.localScale = Vector3.MoveTowards(_trans.localScale, _target, Time.deltaTime * _speed);
        }
    }

    private void Awake()
    {
        _target = _portalSize[0];
    }

    /// <summary>
    /// Включение портала
    /// </summary>
    public void OnPortal()
    {
        _target = _portalSize[1];
    }

    /// <summary>
    /// Выключение порталов
    /// </summary>
    public void OffPortal()
    {
        _target = _portalSize[0];
    }

    /// <summary>
    /// Отображение монстров
    /// </summary>
    /// <param name="var"></param>
    public void ColorPortal(int var)
    {
        switch(var)
        {
            case 0: _mesh.material.SetColor("_EmissionColor", Color.green); break;
            case 1: _mesh.material.SetColor("_EmissionColor", Color.cyan); break;
            case 2: _mesh.material.SetColor("_EmissionColor", Color.red); break;
        }
    }
}
