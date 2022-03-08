using System.Collections.Generic;
using UnityEngine;

public class MonoCache : MonoBehaviour
{
    public static List<MonoCache> allFixedUpdate = new List<MonoCache>(10001);
    public static List<MonoCache> allUpdate = new List<MonoCache>(10001);

    private void OnEnable()
    {
        allFixedUpdate.Add(this);
        allUpdate.Add(this);
    }
    private void OnDisable()
    {
        allFixedUpdate.Remove(this);
        allUpdate.Remove(this);
    }
    private void OnDestroy()
    {
        allFixedUpdate.Remove(this);
        allUpdate.Remove(this);
    }

    public void FixedTick() => OnFixedTick();

    public virtual void OnFixedTick()
    {

    }

    public void Tick() => OnTick();

    public virtual void OnTick()
    {

    }
}
