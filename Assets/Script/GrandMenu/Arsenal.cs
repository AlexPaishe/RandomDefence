using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Arsenal : MonoBehaviour
{
    [SerializeField] private Image[] _selectIconTowerType;
    [SerializeField] private Image[] _selectIconTowerCard;
    [SerializeField] private Text _towerTitle;
    [SerializeField] private string[] _towerName;
    [SerializeField] private Image[] _towerCard;
    [SerializeField] private Sprite[] _towerType;
    private int _type = -1;
    private int[] _deck = new int[5] { 0, 1, 2, 3, 4 };
    private int _card = -1;
    private void Awake()
    {
        SearchDeck();
        ClearIcon();
    }

    /// <summary>
    /// Очистка всех выбраных ячеек
    /// </summary>
    private void ClearIcon()
    {
        for(int i = 0; i < _selectIconTowerCard.Length; i++)
        {
            _selectIconTowerCard[i].enabled = false;
        }

        for(int i = 0; i < _selectIconTowerType.Length;i++)
        {
            _selectIconTowerType[i].enabled = false;
        }

        _type = -1;
        _card = -1;

        for (int i = 0; i < _deck.Length; i++)
        {
            if (_deck[i] == -1)
            {
                _towerCard[i].enabled = false;
            }
            else
            {
                _towerCard[i].enabled = true;
                _towerCard[i].sprite = _towerType[_deck[i]];
            }
        }
    }

    /// <summary>
    /// Выбор иконки башни
    /// </summary>
    /// <param name="var"></param>
    public void SelectTowerType(int var)
    {
        _type = var;
        for(int i = 0; i < _selectIconTowerType.Length; i++)
        {
            if(i == var)
            {
                _selectIconTowerType[i].enabled = true;
            }
            else
            {
                _selectIconTowerType[i].enabled = false;
            }
        }
        _towerTitle.text = _towerName[var];
    }

    /// <summary>
    /// Выбор иконки для карты в колоде
    /// </summary>
    /// <param name="var"></param>
    public void SelectTowerCard(int var)
    {
        _card = var;
        for(int i = 0; i < _selectIconTowerCard.Length; i++)
        {
            if(i == var)
            {
                _selectIconTowerCard[i].enabled = true;
            }
            else
            {
                _selectIconTowerCard[i].enabled = false;
            }
        }
    }

    /// <summary>
    /// Реализация добавления башни в колоду
    /// </summary>
    public void AddTowerInDeck()
    {
        if(_type != -1 && _card != -1)
        {
            for(int i = 0; i < _deck.Length; i++)
            {
                if(i == _card)
                {
                    _deck[i] = _type;
                    _towerCard[i].enabled = true;
                    _towerCard[i].sprite = _towerType[_type];
                    PlayerPrefs.SetInt($"Tower{i}", _type);
                    continue;
                }
                else if(_type == _deck[i])
                {
                    _deck[i] = -1;
                    PlayerPrefs.SetInt($"Tower{i}", -1);
                    _towerCard[i].enabled = false;
                }
            }
        }
    }

    /// <summary>
    /// Проверка доски
    /// </summary>
    /// <returns></returns>
    public bool CheckDeck()
    {
        for(int i = 0; i < _deck.Length; i++)
        {
            if(_deck[i] == -1)
            {
                return true;
            }
        }
        return false;
    }

    /// <summary>
    /// Поиск доски
    /// </summary>
    private void SearchDeck()
    {
        int first = PlayerPrefs.GetInt("First");
        if(first == 0)
        {
            PlayerPrefs.SetInt("First", 1);
            for(int i = 0; i < _deck.Length; i++)
            {
                _deck[i] = -1;
                PlayerPrefs.SetInt($"Tower{i}", -1);
            }
        }
        else
        {
            for(int i = 0; i < _deck.Length; i++)
            {
                _deck[i] = PlayerPrefs.GetInt($"Tower{i}");
            }
        }
    }
}
