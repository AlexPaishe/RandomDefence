using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] private GameObject _bullet;
    [SerializeField] private float _speed;
    [SerializeField] private int _level;
    public float multiply = 1;

    /// <summary>
    /// Реализация выстрела
    /// </summary>
    public void Shoot()
    {
        GameObject bullet = Instantiate(_bullet, transform.position, Quaternion.identity);
        bullet.GetComponent<Rigidbody>().velocity = transform.TransformDirection(Vector3.forward * _speed);
        switch(bullet.GetComponent<BulletDamage>().type)
        {
            case 0:
                bullet.GetComponent<BulletDamage>().damage = Base.KM[_level] * multiply;
                break;
            case 1:
                bullet.GetComponent<BulletDamage>().damage = Base.EL[_level] * multiply;
                break;
            case 2:
                bullet.GetComponent<BulletDamage>().damage = Base.KP[_level] * multiply;
                break;
            case 3:
                bullet.GetComponent<BulletDamage>().damage = Base.EP[_level] * multiply;
                break;
        }
    }

    /// <summary>
    /// Реализация повышения уровня пуль
    /// </summary>
    public void UpgradeShoot()
    {
        _level++;
        if(_level == 3)
        {
            _level = 2;
        }
    }

    /// <summary>
    /// Реализация возращение уровня пули на исходные
    /// </summary>
    public void Downgrade()
    {
        _level = 0;
    }
}
