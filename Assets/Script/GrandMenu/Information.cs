using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Information : MonoBehaviour
{
    [SerializeField] private Text[] _statText;
    [SerializeField] private string[] _speed;
    [SerializeField] private string[] _damage;
    [SerializeField] private string[] _distance;
    [SerializeField] private string[] _health;

    /// <summary>
    /// Реализация получения информации о башне
    /// </summary>
    /// <param name="var"></param>
    public void InfoThisType(int var)
    {
        _statText[0].text = _damage[var];
        _statText[1].text = _speed[var];
        _statText[2].text = _distance[var];
        _statText[3].text = _health[var];
    }
}
