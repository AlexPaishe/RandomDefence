using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeTower : MonoBehaviour
{
    [SerializeField] private TowerSearchEnemy _towerClassic;
    [SerializeField] private SpeedBoosterTower _towerBoost;
    [SerializeField] private ShildTower _towerShild;
    [SerializeField] private Flamer _towerFlamer;
    [SerializeField] private LaserTower _towerLaser;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<UpgradeMove>())
        {
            if(_towerClassic != null)
            {
                _towerClassic.Upgrade();
            }
            else if(_towerBoost != null)
            {
                _towerBoost.Upgrade();
            }
            else if(_towerShild != null)
            {
                _towerShild.Upgrade();
            }
            else if(_towerFlamer != null)
            {
                _towerFlamer.Upgrade();
            }
            else if(_towerLaser != null)
            {
                _towerLaser.Upgrade();
            }
            Debug.Log("Upgrade");
            other.gameObject.SetActive(false);
        }
    }
}
