using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EggSystem : MonoBehaviour
{
    [SerializeField] private EggCell[] _enemyEgg;
    [SerializeField] private EggCell[] _towerEgg;
    private Vector3[] _pointNext = new Vector3[2];
    public int Line;

    /// <summary>
    /// Передача пути для яйца
    /// </summary>
    /// <param name="var"></param>
    /// <returns></returns>
    public Vector3[] EggPoint(bool var)
    {
        if(var == true)
        {
            int rand = Random.Range(0, _enemyEgg.Length);
            _pointNext[0] = _enemyEgg[rand].pointThird.position;
            _pointNext[1] = _enemyEgg[rand].pointFinish.position;
            Line = _enemyEgg[rand].line;
        }
        else
        {
            int rand = Random.Range(0, _towerEgg.Length);
            _pointNext[0] = _towerEgg[rand].pointThird.position;
            _pointNext[1] = _towerEgg[rand].pointFinish.position;
            Line = _enemyEgg[rand].line;
        }
        return _pointNext;
    }

}
