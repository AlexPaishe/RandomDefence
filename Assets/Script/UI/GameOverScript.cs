using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameMenu;
    [SerializeField] private BigMoneySystem _bigMoney;

    private void Awake()
    {
        Time.timeScale = 1;
        _gameMenu[0].SetActive(true);
        _gameMenu[1].SetActive(false);
    }

    /// <summary>
    /// ?????????? ??????????? ??????
    /// </summary>
    public void Restart()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            _gameMenu[0].SetActive(false);
            _gameMenu[1].SetActive(true);
            _bigMoney.GameOver();
            Time.timeScale = 0;
        }
        else if(other.GetComponent<BossHealth>())
        {
            _gameMenu[0].SetActive(false);
            _gameMenu[1].SetActive(true);
            _bigMoney.GameOver();
            Time.timeScale = 0;
        }
    }
}
