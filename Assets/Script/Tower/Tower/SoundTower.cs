using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTower : MonoBehaviour
{
    [SerializeField] private AudioSource[] _fireGun;
    [SerializeField] private AudioClip[] _shoot;
    [SerializeField] private AudioSource _explosion;
    [SerializeField] private AudioClip[] _explosionVariation;

    /// <summary>
    /// Реализация взрыва
    /// </summary>
    public void Explosion()
    {
        _explosion.Stop();
        int rand = Random.Range(0, _explosionVariation.Length);
        _explosion.clip = _explosionVariation[rand];
        _explosion.Play();
    }

    /// <summary>
    /// Реализация звука выстрела
    /// </summary>
    /// <param name="var"></param>
    public void Shoot(int var)
    {
        _fireGun[var].Stop();
        int rand = Random.Range(0, _shoot.Length);
        _fireGun[var].clip = _shoot[rand];
        _fireGun[var].Play();
    }

    /// <summary>
    /// Остановка звука выстрела
    /// </summary>
    public void StopShoot()
    {
        _fireGun[0].Stop();
    }
}
