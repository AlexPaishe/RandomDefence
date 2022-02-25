using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBooster : MonoBehaviour
{
    [SerializeField] private float _gain;
    private int _level;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<TowerSearchEnemy>())
        {
            other.GetComponent<TowerSearchEnemy>().BoosterTime(_gain);
        }
        else if (other.GetComponent<Flamer>())
        {
            other.GetComponent<Flamer>().BoostTimer(_gain);
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
