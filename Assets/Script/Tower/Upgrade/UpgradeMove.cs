using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeMove : MonoCache
{
    public Vector3 Target;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;

    public override void OnFixedTick()
    {
        _trans.position = Vector3.MoveTowards(_trans.position, Target, _speed * Time.fixedDeltaTime);
        if (_trans.position == Target)
        {
            gameObject.SetActive(false);
        }
    }
}
