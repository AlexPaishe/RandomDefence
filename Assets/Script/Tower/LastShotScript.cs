using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastShotScript : MonoCache
{
    public void DoLastShoot()
    {
        Collider[] shootArea =  Physics.OverlapBox(transform.position, new Vector3(0.5f, 2.7f, 5), Quaternion.identity);
        foreach (var item in shootArea)
        {
            if (item.gameObject.TryGetComponent(out EnemyHealth health))
            {
                health.Health -= health.Health;

            }
        }
    }
}
