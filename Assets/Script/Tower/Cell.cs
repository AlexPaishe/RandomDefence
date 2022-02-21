using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public bool Select = false;
    public Transform TowerPlace;
    public int _type;
    public int Disk;
    public int Type
    {
        get
        {
            return _type;
        }
        
        set
        {
            _type = value;
            if(_type > 5)
            {
                _type = 0;
            }
            else if(_type < 0)
            {
                _type = 5;
            }
        }
    }
}
