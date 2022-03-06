using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private Text[] _text;
    [SerializeField] private int _sellTower;
    private int _money;
    private int _towerPrice;
    public int Money
    {
        get
        {
            return _money;
        }
        set
        {
            _money = value;
            _text[2].text = $"Money / {_money}";
        }
    }

    public int TowerPrice
    {
        get
        {
            return _towerPrice;
        }
        set
        {
            _towerPrice = value;
            _text[0].text = $"Tower / {_towerPrice}";
        }
    }

    private void Awake()
    {
        Money = Base.Money;
        TowerPrice = Base.TowerPrice;
    }

    /// <summary>
    /// Реализация возможности постройки башни
    /// </summary>
    /// <returns></returns>
    public bool TowerBuild()
    {
        if(Money - TowerPrice >= 0)
        {
            Money -= TowerPrice;
            TowerPrice += _sellTower;
            return true;
        }
        return false;
    }
}
