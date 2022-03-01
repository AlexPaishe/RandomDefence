using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClassicBullet : MonoCache
{
    public Vector3 target;
    [SerializeField] private float _speed;
    [SerializeField] private Transform _trans;
    [SerializeField] private bool _trap;

    public override void OnTick()
    {
        _trans.position = Vector3.MoveTowards(_trans.position, target, _speed * Time.deltaTime);
        if(_trap == false && _trans.position == target)
        {
            Destroy(gameObject);
        }
    }
}
