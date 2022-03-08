using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySound : MonoBehaviour
{
    [SerializeField] private AudioSource _source;
    [SerializeField] private AudioClip[] _mosterSoundBegin;
    [SerializeField] private AudioClip[] _monsterSoundDamage;    

    /// <summary>
    /// Повлениея
    /// </summary>
    public void Begin()
    {
        _source.Stop();
        int rand = Random.Range(0, _monsterSoundDamage.Length);
        _source.volume = 1f;
        _source.clip = _mosterSoundBegin[rand];
        _source.Play();
    }

    /// <summary>
    /// Смерть
    /// </summary>
    public void Death()
    {
        _source.Stop();
        int rand = Random.Range(0, _monsterSoundDamage.Length);
        _source.volume = 0.8f;
        _source.clip = _monsterSoundDamage[rand];
        _source.Play();
    }
}
