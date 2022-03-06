using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private Slider _bossHealthSlider;
    [SerializeField] private float _health;
    [SerializeField] private EnemySpawn _enemySpawn;
    private int _count = 0;
    private int _kill = 0;
    public int Kills
    {
        get
        {
            return _kill;
        }
        set
        {
            _kill = value;
            _bossHealthSlider.value = _kill;
            if(_kill == _count)
            {

            }
        }
    }
    private void Awake()
    {
        _count =_enemySpawn.SliderInd();
        _bossHealthSlider.maxValue = _count;
    }
}
