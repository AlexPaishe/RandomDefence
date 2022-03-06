using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _icons;
    [SerializeField] private GameObject _pause;
    [SerializeField] private GameObject _setting;
    [SerializeField] private MoneySystem _money;
    [SerializeField] private ColorDisk _disk;

    /// <summary>
    /// ���������� ��������� ������ �� ���� ������ �� ����
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
    /// ���������� ��������� ������ �� ���� ����� �� ����
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
    }

    /// <summary>
    /// ���������� �����
    /// </summary>
    public void Pause()
    {
        if(_pause.activeSelf || _setting.activeSelf)
        {
            Time.timeScale = 1;
            _pause.SetActive(false);
            _setting.SetActive(false);
        }
        else if(_pause.activeSelf == false && _setting.activeSelf == false)
        {
            Time.timeScale = 0;
            _pause.SetActive(true);
        }
    }

    /// <summary>
    /// ���������� � ����
    /// </summary>
    public void BackMenu()
    {
        SceneManager.LoadScene(0);
    }

    /// <summary>
    /// ���������� ��������� ���� ��������
    /// </summary>
    public void Setting()
    {
        _pause.SetActive(false);
        _setting.SetActive(true);
    }

    /// <summary>
    /// ���������� ���������� ���� ��������
    /// </summary>
    public void BackPauseMenu()
    {
        _pause.SetActive(true);
        _setting.SetActive(false);
    }
}
