using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBoosterTower : MonoBehaviour
{
    [SerializeField] private BoxCollider _box;
    [SerializeField] private SpeedBooster _speed;
    [SerializeField] private float[] _zoneBoost;

    /// <summary>
    /// Реализация зону действия ускорителя
    /// </summary>
    public void BoostZone()
    {
        Vector3 posBox = _box.center;
        posBox.x = _zoneBoost[Base.Disk];
        _box.center = posBox;
    }

    /// <summary>
    /// Реализация апгрейда
    /// </summary>
    public void Upgrade()
    {
        _speed.Upgrade();
    }

    /// <summary>
    /// Реализация возврата на базовые настройки
    /// </summary>
    public void Downgrade()
    {
        _speed.Downgrade();
    }
}
