using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _click;
    [SerializeField] private AudioSource _soundClick;

    /// <summary>
    /// Реализация клика 
    /// </summary>
    public void ClickSelect(int var)
    {
        _soundClick.clip = _click[var];
        _soundClick.Play();
    }
}
