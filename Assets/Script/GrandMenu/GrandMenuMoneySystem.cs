using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GrandMenuMoneySystem : MonoBehaviour
{
    [SerializeField] private Text _moneyText;
    private int _money;
    public int Money
    {
        get
        {
            return _money;
        }

        set
        {
            _money = value;
            _moneyText.text = $"{_money}";
            PlayerPrefs.SetInt("BigMoney", _money);
        }
    }

    private void Awake()
    {
        Money = PlayerPrefs.GetInt("BigMoney");
    }
}
