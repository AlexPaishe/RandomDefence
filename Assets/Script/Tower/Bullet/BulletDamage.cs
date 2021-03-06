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
        else if(other.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
        else if(other.GetComponent<BossHealth>())
        {
            BossHealth hp = other.GetComponent<BossHealth>();
            if(hp.Health > 0)
            {
                hp.Health -= damage;
                Destroy(this.gameObject);
            }
        }
    }
}
