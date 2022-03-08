using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    [SerializeField] private float _gain;
    private int _level;
    public float multiply = 1;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TowerSearchEnemy>())
        {
            other.GetComponent<TowerSearchEnemy>().BoosterTime(_gain * multiply);
        }
        else if (other.GetComponent<Flamer>())
        {
            other.GetComponent<Flamer>().BoostTimer(_gain * multiply);
        }
        else if(other.GetComponent<LaserTower>())
        {
            other.GetComponent<LaserTower>().BoosterTime(_gain * multiply);
        }
        else if(other.GetComponent<Shild>())
        {
            other.GetComponent<Shild>().BoosterTime(_gain * multiply);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.GetComponent<TowerSearchEnemy>())
        {
            other.GetComponent<TowerSearchEnemy>().ClassicTime();
        }
        else if(other.GetComponent<Flamer>())
        {
            other.GetComponent<Flamer>().ClassicTimer();
        }
        else if (other.GetComponent<LaserTower>())
        {
            other.GetComponent<LaserTower>().ClassicTime();
        }
        else if (other.GetComponent<Shild>())
        {
            other.GetComponent<Shild>().ClassicTime();
        }
    }

    private void Start()
    {
        _gain = Base.SB[0];
    }

    /// <summary>
    /// Реализация апгрейда
    /// </summary>
    public void Upgrade()
    {
        _level++;
        if(_level == 3)
        {
            _level = 2;
        }
        _gain = Base.SB[_level];
    }

    /// <summary>
    /// Реализация возрата на базовые настройки
    /// </summary>
    public void Downgrade()
    {
        _level = 0;
        _gain = Base.SB[_level];
    }
}
