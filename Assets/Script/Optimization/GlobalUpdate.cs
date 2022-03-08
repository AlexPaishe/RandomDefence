using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalUpdate : MonoBehaviour
{
    private void FixedUpdate()
    {
        for(int i = 0; i < MonoCache.allFixedUpdate.Count; i++)
        {
            MonoCache.allFixedUpdate[i].FixedTick();
        }
    }

    private void Update()
    {
        for (int i = 0; i < MonoCache.allUpdate.Count; i++)
        {
            MonoCache.allUpdate[i].Tick();
        }
    }
}
