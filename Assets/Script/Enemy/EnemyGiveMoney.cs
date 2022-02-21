using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyGiveMoney : MonoBehaviour
{
    [SerializeField] private int _priceKill;
    private MoneySystem _money;

    private void Awake()
    {
        _money = FindObjectOfType<MoneySystem>();
    }

    /// <summary>
    /// РЕализация вознаграждения за убийство монстров
    /// </summary>
    public void GiveMoney()
    {
        _money.Money += _priceKill;
    }
}
