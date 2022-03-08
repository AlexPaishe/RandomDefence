using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CellSystem : MonoBehaviour
{
    public Cell[] _cells;
    [SerializeField] private int _type;
    [SerializeField] private List<Cell> _system;


    /// <summary>
    /// «апись изменени€ клеток при вращении колец
    /// </summary>
    public void CellThisSystem()
    {
        for(int i = 0; i < _system.Count; i++)
        {
            if(_system[i].Type == _type)
            {
                int disk = _system[i].Disk;
                _cells[disk] = _system[i];
            }
        }
    }
}
