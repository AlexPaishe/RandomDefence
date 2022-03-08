using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BigMoneySystem : MonoBehaviour
{
    [SerializeField] private Text[] _infoText;
    public int[] infoState;

    private void Awake()
    {
        infoState[0] = PlayerPrefs.GetInt("BigMoney");
        Debug.Log(infoState[0]);
    }

    /// <summary>
    /// Реализация конца игры
    /// </summary>
    public void GameOver()
    {
        infoState[0] += infoState[1];
        PlayerPrefs.SetInt("BigMoney", infoState[0]);
        for(int i = 0; i < _infoText.Length; i++)
        {
            _infoText[i].text = $"{infoState[i]}";
        }
    }
}
