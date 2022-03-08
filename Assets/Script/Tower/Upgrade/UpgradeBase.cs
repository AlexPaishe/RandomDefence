using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeBase : MonoBehaviour
{
    [SerializeField] private float[] _damageKineticMinigun = new float[3];
    [SerializeField] private float[] _damageEnergyLaser = new float[3];
    [SerializeField] private float[] _damageKineticPWO = new float[3];
    [SerializeField] private float[] _damageEnergyPWO = new float[3];
    [SerializeField] private int[] _healthShild = new int[3];
    [SerializeField] private float[] _speedBoost = new float[3];
    [SerializeField] private float[] _flamerFire = new float[3];

    private void Awake()
    {
        Base.KM = _damageKineticMinigun;
        Base.EL = _damageEnergyLaser;
        Base.KP = _damageKineticPWO;
        Base.EP = _damageEnergyPWO;
        Base.ES = _healthShild;
        Base.SB = _speedBoost;
        Base.CF = _flamerFire;
    }
}
