using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoneySystem : MonoBehaviour
{
    [SerializeField] private Text[] _text;
    [SerializeField] private int _sellTower;
    [SerializeField] private int _sellUpgrade;
    private int _money;
    private int _towerPrice;
    private int _modulPrice;
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

    public int ModulPrice
    {
        get
        {
            return _modulPrice;
        }
        set
        {
            _modulPrice = value;
            _text[1].text = $"Upgrade / {_modulPrice}";
        }
    }

    private void Awake()
    {
        Money = Base.Money;
        TowerPrice = Base.TowerPrice;
        ModulPrice = Base.UpgradePrice;
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

    /// <summary>
    /// РЕализация возможности покупки улучшения
    /// </summary>
    /// <returns></returns>
    public bool UpgradeBuild()
    {
        if(Money - ModulPrice >= 0)
        {
            Money -= ModulPrice;
            ModulPrice += _sellUpgrade;
            return true;
        }
        return false;
    }
}
