using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoCache
{
    private bool _go;
    private Transform _firePoint;
    [SerializeField] private LineRenderer _lineLaser;
    public bool Go
    {
        get
        {
            return _go;
        }

        set
        {
            _go = value;
            if(_go == false)
            {
                _lineLaser.SetPosition(_lineLaser.positionCount - 2, transform.position);
                _lineLaser.SetPosition(_lineLaser.positionCount - 1, transform.position);
            }
        }
    }

    public override void OnTick()
    {
        if (_go == true)
        {
            if (_firePoint != null)
            {
                _lineLaser.SetPosition(_lineLaser.positionCount - 2, transform.position);
                _lineLaser.SetPosition(_lineLaser.positionCount - 1, _firePoint.transform.position);
            }
            else
            {
                _lineLaser.SetPosition(_lineLaser.positionCount - 1, transform.position);
                _lineLaser.SetPosition(_lineLaser.positionCount - 2, transform.position);
            }
        }
    }

    /// <summary>
    /// Реализация лазера
    /// </summary>
    /// <param name="go"></param>
    /// <param name="target"></param>
    public void FirePoint(bool go, Transform target)
    {
        if(go == false)
        {
            Go = false;
            _firePoint = null;
        }
        else
        {
            Go = true;
            _firePoint = target;
        }
    }
}
