using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateCamera : MonoCache
{
    [SerializeField] private Transform _trans;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _maxDistance;
    [SerializeField] private Transform _cameraTrans;
    private bool _go = false;
    private float _nowRotate = 0;
    private float _newRotate = 0;

    public override void OnFixedTick()
    {
        if (_go == true)
        {
            RotateCam();
        }
    }

    /// <summary>
    /// Реализация Поворота камеры влево
    /// </summary>
    public void PlusNewRotate()
    {
        _go = true;
        _newRotate += _angle;
    }

    /// <summary>
    /// Реализация поворота камеры вправо
    /// </summary>
    public void MinusNewRotate()
    {
        _go = true;
        _newRotate -= _angle;
    }

    /// <summary>
    /// Реализация поворота камеры
    /// </summary>
    private void RotateCam()
    {
        if (_nowRotate != _newRotate)
        {
            if(_nowRotate > _newRotate)
            {
                if(_nowRotate - _newRotate > _maxDistance)
                {
                    _nowRotate -= _speed * Time.fixedDeltaTime;
                    _trans.localEulerAngles = new Vector3(0, _nowRotate, 0);
                }
                else if(_nowRotate - _newRotate <= _maxDistance)
                {
                    _nowRotate = _newRotate;
                    _trans.localEulerAngles = new Vector3(0, _nowRotate, 0);
                }
            }
            else if (_nowRotate < _newRotate)
            {
                if (_newRotate - _nowRotate > _maxDistance)
                {
                    _nowRotate += _speed * Time.fixedDeltaTime;
                    _trans.localEulerAngles = new Vector3(0, _nowRotate, 0);
                }
                else if (_newRotate - _nowRotate <= _maxDistance)
                {
                    _nowRotate = _newRotate;
                    _trans.localEulerAngles = new Vector3(0, _nowRotate, 0);
                }
            }
        }
        else
        {
            _go = false;
        }
    }

    /// <summary>
    /// Реализация зума
    /// </summary>
    /// <param name="var"></param>
    public void SliderZoom(float var)
    {
        _cameraTrans.localPosition = new Vector3(0, 0, var);
    }
}
