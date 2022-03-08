using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoCache
{
    [SerializeField] private GameObject[] _icons;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _setting;
    [SerializeField] private MoneySystem _money;
    [SerializeField] private ColorDisk _disk;
    [SerializeField] private float _pauseTime;
    private float _time;
    private bool _goPause = false;

    public float Timer
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
            if(_time >= 1)
            {
                _time = 1;
                Time.timeScale = 1;
                _goPause = false;
            }
        }
    }

    /// <summary>
    /// Реализация изменения кольца на диск дальше от базы
    /// </summary>
    public void DiskUp()
    {
        Base.Disk++;
        if(Base.Disk > 2)
        {
            Base.Disk = 0;
        }
        _disk.Disk = Base.Disk;
        for(int i = 0; i < 3; i++)
        {
            if(i == Base.Disk)
            {
                _icons[i].SetActive(true);
                _icons[i + 3].SetActive(true);
            }
            else
            {
                _icons[i].SetActive(false);
                _icons[i + 3].SetActive(false);
            }
        }
    }

    /// <summary>
    /// РЕализация изменения кольца на диск ближе от базы
    /// </summary>
    public void DiskDown()
    {
        Base.Disk--;
        if (Base.Disk < 0)
        {
            Base.Disk = 2;
        }
        _disk.Disk = Base.Disk;
        for (int i = 0; i < 3; i++)
        {
            if (i == Base.Disk)
            {
                _icons[i].SetActive(true);
                _icons[i + 3].SetActive(true);
            }
            else
            {
                _icons[i].SetActive(false);
                _icons[i + 3].SetActive(false);
            }
        }
    }

    private void Awake()
    {
        Time.timeScale = 1;
        for (int i = 0; i < 3; i++)
        {
            if (i == Base.Disk)
            {
                _icons[i].SetActive(true);
                _icons[i + 3].SetActive(true);
            }
            else
            {
                _icons[i].SetActive(false);
                _icons[i + 3].SetActive(false);
            }
        }
        _time = _pauseTime;
    }

    /// <summary>
    /// Реализация паузы
    /// </summary>
    public void Pause()
    {
        if(_pause.activeSelf || _setting.activeSelf)
        {
            _goPause = true;
            _pause.SetActive(false);
            _setting.SetActive(false);
        }
        else if(_pause.activeSelf == false && _setting.activeSelf == false)
        {
            Timer = 0;
            Time.timeScale = Timer;
            _pause.SetActive(true);
        }
    }

    /// <summary>
    /// Возращение в меню
    /// </summary>
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// Реализация включения меню настроек
    /// </summary>
    public void Setting()
    {
        _pause.SetActive(false);
        _setting.SetActive(true);
    }

    /// <summary>
    /// Реализация выключения меню настроек
    /// </summary>
    public void BackPauseMenu()
    {
        _pause.SetActive(true);
        _setting.SetActive(false);
    }

    public override void OnTick()
    {
        if(_goPause == true)
        {
            Timer += Time.realtimeSinceStartup;
            Time.timeScale = Timer;
        }
    }

    /// <summary>
    /// Реализация перезагрузки игры
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(1);
    }
}
