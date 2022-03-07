using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GrandMenu : MonoBehaviour
{
    [SerializeField] private GameObject[] _menu;
    [SerializeField] private Text _errorText;
    [SerializeField] private Arsenal _arsenal;
    [SerializeField] private float _errorTime;
    private float _time;
    private bool _go = false;
    public float Timer
    {
        get
        {
            return _time;
        }

        set
        {
            _time = value;
            if(_time <= 0)
            {
                _time = _errorTime;
                Go = false;
            }
        }
    }

    public bool Go
    {
        get
        {
            return _go;
        }

        set
        {
            _go = value;
            if(_go == false)
            {
                _errorText.enabled = false;
            }
            else
            {
                _errorText.enabled = true;
            }
        }
    }

    /// <summary>
    /// Открытие меню и закрытие других окон меню
    /// </summary>
    /// <param name="var"></param>
    public void OpenMenu(int var)
    {
        for(int i = 0; i < _menu.Length; i++)
        {
            if(i == var)
            {
                _menu[i].SetActive(true);
            }
            else
            {
                _menu[i].SetActive(false);
            }
        }
    }

    /// <summary>
    /// Выключение игры
    /// </summary>
    public void Quit()
    {
        Application.Quit();
    }

    private void Awake()
    {
        Time.timeScale = 1;
        OpenMenu(0);
        _time = _errorTime;
        Go = false;
    }

    private void Update()
    {
        if(Go == true)
        {
            Timer -= Time.deltaTime;
        }
    }

    /// <summary>
    /// Включение игры
    /// </summary>
    public void PlayGame()
    {
        Go = _arsenal.CheckDeck();
        if(Go == false)
        {
            SceneManager.LoadScene(1);
        }
    }
}
