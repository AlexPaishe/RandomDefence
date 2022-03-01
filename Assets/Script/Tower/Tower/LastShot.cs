using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LastShot : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            other.GetComponent<EnemyHealth>().Health = 0;
        }
    }
}
