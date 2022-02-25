using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletDamage : MonoBehaviour
{
    public float damage;
    public int type;

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            EnemyHealth hp = other.GetComponent<EnemyHealth>();
            if (hp.Health > 0)
            {
                hp.Health -= damage;
                Destroy(this.gameObject);
            }
        }
    }
}
