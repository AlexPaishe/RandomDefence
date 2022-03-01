using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [SerializeField] private GameObject[] _gameMenu;

    private void Awake()
    {
        Time.timeScale = 1;
        _gameMenu[0].SetActive(true);
        _gameMenu[1].SetActive(false);
    }

    /// <summary>
    /// Реализация перезагрузк уровня
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            _gameMenu[0].SetActive(false);
            _gameMenu[1].SetActive(true);
            Time.timeScale = 0;
        }
    }
}
