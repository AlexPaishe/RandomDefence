using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
class WayPoint
{
    public Transform[] trans;
}

public class EnemyMoveSystem : MonoBehaviour
{
    [SerializeField] private List<WayPoint> _enemyWay;

    /// <summary>
    /// Возвращение пути
    /// </summary>
    /// <param name="var"></param>
    /// <returns></returns>
    public Transform[] EnemyWayPoint(int var)
    {
        return _enemyWay[var].trans;
    }
}
