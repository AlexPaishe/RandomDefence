using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerLine : MonoBehaviour
{
    [SerializeField] private Shooting[] _shoot;
    [SerializeField] private LaserTower _laser;
    [SerializeField] private SpeedBooster _speed;
    [SerializeField] private Flamer _flame;
    [SerializeField] private ShildTower _shild;
    [SerializeField] private int _type;

    private void OnTriggerStay(Collider other)
    {
        if (other.GetComponent<PlatformLine>())
        {
            if (other.GetComponent<PlatformLine>().go == true)
            {
                switch (_type)
                {
                    case 0:
                        for (int i = 0; i < _shoot.Length; i++)
                        {
                            _shoot[i].multiply = 2;
                        }
                        break;
                    case 1:
                        _laser.multiply = 2;
                        break;
                    case 2:
                        _speed.multiply = 0.5f;
                        break;
                    case 3:
                        _flame.multiply = 2;
                        break;
                    case 4:
                        _shild.multiply = 2;
                        break;
                }
            }
            else if (other.GetComponent<PlatformLine>().go == false)
            {
                switch (_type)
                {
                    case 0:
                        for (int i = 0; i < _shoot.Length; i++)
                        {
                            _shoot[i].multiply = 1;
                        }
                        break;
                    case 1:
                        _laser.multiply = 1;
                        break;
                    case 2:
                        _speed.multiply = 1;
                        break;
                    case 3:
                        _flame.multiply = 1;
                        break;
                    case 4:
                        _shild.multiply = 1;
                        break;
                }
            }
        }
    }
} 
