using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flame : MonoBehaviour
{
    public float damage = 2;

    public void FlameThrower(Collider[] flameArea)
    {
        foreach (var item in flameArea)
        {
            if (item.gameObject.TryGetComponent(out EnemyHealth health))
            {
                health.Health -= damage;

            }
        }
    }
}
