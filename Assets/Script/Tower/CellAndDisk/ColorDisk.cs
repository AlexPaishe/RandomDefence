using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ColorDisk : MonoBehaviour
{
    [SerializeField] private Material[] _mats;
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
            for(int i = 0; i < _mats.Length; i++)
            {
                if(i == _disk)
                {
                    _mats[i].EnableKeyword("_EMISSION");
                }
                else
                {
                    _mats[i].DisableKeyword("_EMISSION");
                }
            }
        }
    }

    private void Awake()
    {
        Disk = 0;
        Base.Disk = Disk;
    }
}
