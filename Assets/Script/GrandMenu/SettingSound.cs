using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingSound : MonoBehaviour
{
    [SerializeField] private AudioClip[] _click;
    [SerializeField] private AudioSource _soundClick;
    [Header("Setting")]
    [SerializeField] private AudioMixerGroup[] _mixer;
    [SerializeField] private Slider[] _sliderSound;
    [SerializeField] private string[] _mixerName;
    [SerializeField] private Image[] _sliderBackground;

    /// <summary>
    /// Реализация клика 
    /// </summary>
    public void ClickSelect(int var)
    {
        _soundClick.clip = _click[var];
        _soundClick.Play();
    }

    private void Awake()
    {
        for(int i = 0; i < _sliderSound.Length; i++)
        {
            _sliderSound[i].value = PlayerPrefs.GetFloat(_mixerName[i]);
            _sliderBackground[i].fillAmount = (_sliderSound[i].value + 20) / 50;
        }
    }

    private void Start()
    {
        for(int i = 0; i < _sliderSound.Length; i++)
        {
            _mixer[i].audioMixer.SetFloat(_mixerName[i], PlayerPrefs.GetFloat(_mixerName[i]));
        }
    }

    /// <summary>
    /// Реализация изменения громкости музыки
    /// </summary>
    /// <param name="val"></param>
    public void SliderMusic(float val)
    {
        PlayerPrefs.SetFloat(_mixerName[0], val);
        _mixer[0].audioMixer.SetFloat(_mixerName[0], val);
        _sliderBackground[0].fillAmount = (val + 20) / 50;
    }

    /// <summary>
    /// Реализация изменения громкость эффектов
    /// </summary>
    /// <param name="val"></param>
    public void SliderEffect(float val)
    {
        PlayerPrefs.SetFloat(_mixerName[1], val);
        _mixer[1].audioMixer.SetFloat(_mixerName[1], val);
        _sliderBackground[1].fillAmount = (val + 20) / 50;
    }
}
