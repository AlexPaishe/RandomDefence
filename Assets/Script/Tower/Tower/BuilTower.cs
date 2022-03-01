using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuilTower : MonoCache
{
    [SerializeField] private LayerMask _layer;
    [SerializeField] private float _maxDistance;
    [SerializeField] private TowerSpawn _tower;
    [SerializeField] private Camera _main;
    [SerializeField] private MoneySystem _money;

    public override void OnTick()
    {
        RaycastHit hit;
        if (Physics.Raycast(_main.ScreenPointToRay(Input.mousePosition), out hit, _maxDistance, _layer) && Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (hit.collider.GetComponent<CellSystem>() != null)
            {
                hit.collider.GetComponent<CellSystem>().CellThisSystem();
                Cell cell = hit.collider.GetComponent<CellSystem>()._cells[Base.Disk];
                if (cell.Select == false && _money.TowerBuild())
                {
                    Transform trans = cell.TowerPlace;
                    _tower.BuildTower(trans);
                    cell.Select = true;
                }
            }
        }
    }
}
