using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskRotate : MonoCache
{
    [SerializeField] private Transform[] _transDisk;
    [SerializeField] private float _speed;
    [SerializeField] private float _angle;
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _minDrag;
    [Header("Cells")]
    [SerializeField] private Cell[] _mini;
    [SerializeField] private Cell[] _medium;
    [SerializeField] private Cell[] _big;
    private float[] _nowAngle = new float[3] { 0, 0, 0 };
    private float[] _newAngle = new float[3] { 0, 0, 0 };

    public void Minus()
    {
        _newAngle[Base.Disk] -= _angle;
        MinusTypeCells();
    }

    public void Plus()
    {
        _newAngle[Base.Disk] += _angle;
        PlusTypeCells();
    }

    public override void OnFixedTick()
    {
        if (_nowAngle[0] != _newAngle[0] || _nowAngle[1] != _newAngle[1] || _nowAngle[2] != _newAngle[2])
        {
            RotateOneDisk();
        }
    }

    /// <summary>
    /// Реализация изменения всех клеток на круг в поворот влево
    /// </summary>
    public void MinusTypeCells()
    {
        switch(Base.Disk)
        {
            case 0:
                for(int i = 0; i < _mini.Length; i++)
                {
                    _mini[i].Type--;
                }
                break;
            case 1:
                for (int i = 0; i < _medium.Length; i++)
                {
                    _medium[i].Type--;
                }
                break;
            case 2:
                for (int i = 0; i < _big.Length; i++)
                {
                    _big[i].Type--;
                }
                break;
        }
    }

    /// <summary>
    /// Реализация изменения всех клеток на круг в поворот вправо
    /// </summary>
    public void PlusTypeCells()
    {
        switch (Base.Disk)
        {
            case 0:
                for (int i = 0; i < _mini.Length; i++)
                {
                    _mini[i].Type++;
                }
                break;
            case 1:
                for (int i = 0; i < _medium.Length; i++)
                {
                    _medium[i].Type++;
                }
                break;
            case 2:
                for (int i = 0; i < _big.Length; i++)
                {
                    _big[i].Type++;
                }
                break;
        }
    }

    /// <summary>
    /// Реализация поворота диска
    /// </summary>
    public void RotateOneDisk()
    {
        for (int i = 0; i < 3; i++)
        {
            if (_nowAngle[i] > _newAngle[i])
            {
                if (_nowAngle[i] - _newAngle[i] > _maxDistance)
                {
                    _nowAngle[i] -= _speed * Time.fixedDeltaTime;
                    _transDisk[i].localEulerAngles = new Vector3(0, _nowAngle[i], 0);
                }
                else if (_nowAngle[i] - _newAngle[i] <= _maxDistance)
                {
                    _nowAngle[i] = _newAngle[i];
                    _transDisk[i].localEulerAngles = new Vector3(0, _nowAngle[i], 0);
                }
            }
            else if (_nowAngle[i] < _newAngle[i])
            {
                if (_newAngle[i] - _nowAngle[i] > _maxDistance)
                {
                    _nowAngle[i] += _speed * Time.fixedDeltaTime;
                    _transDisk[i].localEulerAngles = new Vector3(0, _nowAngle[i], 0);
                }
                else if (_newAngle[i] - _nowAngle[i] <= _maxDistance)
                {
                    _nowAngle[i] = _newAngle[i];
                    _transDisk[i].localEulerAngles = new Vector3(0, _nowAngle[i], 0);
                }
            }
        }
    }
}
