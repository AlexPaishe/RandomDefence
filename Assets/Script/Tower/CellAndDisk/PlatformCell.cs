using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCell : MonoCache
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Texture _emissionMap;
    public int disk;
    private int _type;
    public int Type
    {
        get
        {
            return _type;
        }
        set
        {
            _type = value;
            switch(_type)
            {
                case 0: _mesh.material.SetColor("_EmissionColor", Color.red); break;
                case 1: _mesh.material.SetColor("_EmissionColor", Color.green); break;
                case 2: _mesh.material.SetColor("_EmissionColor", Color.blue); break;
            }
        }
    }

    private void Awake()
    {
        _mesh.material.SetTexture("_EmissionMap", _emissionMap);
    }

    /// <summary>
    /// Реализация включения линии
    /// </summary>
    /// <param name="var"></param>
    public void LineCell(float var)
    {
        _mesh.material.SetFloat("_MultyColor", var);
    }
}
