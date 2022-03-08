using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDisk : MonoBehaviour
{
    [SerializeField] private MeshRenderer[] _mesh;
    [SerializeField] private Texture[] _emissionMask;
    private int _disk;
    public int Disk
    {
        get
        {
            return _disk;
        }

        set
        {
            _disk = value;
            for(int i = 0; i < _mesh.Length; i++)
            {
                _mesh[i].material.SetTexture("_EmissionMap", _emissionMask[_disk]);
            }
        }
    }

    private void Awake()
    {
        Disk = 0;
        Base.Disk = Disk;
    }
}
