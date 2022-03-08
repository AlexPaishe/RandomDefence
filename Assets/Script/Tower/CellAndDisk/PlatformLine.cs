using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLine : MonoBehaviour
{
    public bool go = false;
    private int[] _disk = new int[3] { -1, -1, -1 };
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlatformCell>())
        {
            PlatformCell cell = other.GetComponent<PlatformCell>();
            _disk[cell.disk] = cell.Type;
            Line();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<PlatformCell>())
        {
            if(go == false)
            {
                other.GetComponent<PlatformCell>().LineCell(1);
            }
            else
            {
                other.GetComponent<PlatformCell>().LineCell(2f);
            }
        }
    }

    /// <summary>
    /// Реализация линии из клеток
    /// </summary>
    private void Line()
    {
        if(_disk[0] == _disk[1]&& _disk[1] == _disk[2] && _disk[0] != -1 && _disk[1] != -1&& _disk[2] !=-1)
        {
            go = true;
        }
        else
        {
            go = false;
        }
    }
}
