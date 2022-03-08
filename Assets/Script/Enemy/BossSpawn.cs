using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BossSpawn : MonoBehaviour
{
    [SerializeField] private Slider _bossHealthSlider;
    [SerializeField] private EnemySpawn _enemySpawn;
    [SerializeField] private Portal[] _portals;
    [SerializeField] private Transform[] _firstTarget;
    [SerializeField] private Transform[] _spawn;
    [SerializeField] private GameObject _boss;
    [SerializeField] private Image _bossTitle;
    [SerializeField] private AudioSource _music;
    [SerializeField] private AudioClip[] _sound;
    private GameObject _bossObj;
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
                _enemySpawn.boss = true;
                Begin();
            }
        }
    }
    private void Awake()
    {
        _count =_enemySpawn.SliderInd();
        _bossHealthSlider.maxValue = _count;
        _bossObj = Instantiate(_boss, _spawn[0].position, Quaternion.identity);
        _bossObj.SetActive(false);
        _bossTitle.enabled = false;
    }

    /// <summary>
    /// Начало битвы с босом
    /// </summary>
    private void Begin()
    {
        int rand = Random.Range(0, 5);
        for(int i = 0; i < _portals.Length; i++)
        {
            if(i == rand)
            {
                _portals[i].OnPortal();
                _portals[i].ColorPortal(2);
            }
            else
            {
                _portals[i].OffPortal();
            }
        }

        _bossObj.SetActive(true);
        _bossObj.transform.position = _spawn[rand].position;
        BossMove bossMove = _bossObj.GetComponent<BossMove>();
        bossMove.FirstTarget(_firstTarget[rand].position);
        bossMove.go = true;
        BossHealth bossHP = _bossObj.GetComponent<BossHealth>();
        bossHP.Health = _count * 15;
        bossHP.NewPrice(_count);
        bossHP.BossBeginSound();
        _bossHealthSlider.maxValue = bossHP.Health;
        _bossTitle.enabled = true;
        _music.Stop();
        _music.clip = _sound[1];
        _music.Play();
    }

    /// <summary>
    /// Реализация победы над босом и начала новой эры
    /// </summary>
    public void BossWave()
    {
        _count = _enemySpawn.SliderInd();
        _bossHealthSlider.maxValue = _count;
        Kills = 0;
    }

    /// <summary>
    /// Показание хп босса
    /// </summary>
    /// <param name="var"></param>
    public void BossHealthSlider(float var)
    {
        _bossHealthSlider.value = var;
        if(_bossHealthSlider.value == 0)
        {
            _bossTitle.enabled = false;
        }
    }

    /// <summary>
    /// Окончание битвы с босом
    /// </summary>
    public void BossFightEnd()
    {
        _music.Stop();
        _music.clip = _sound[0];
        _music.Play();
    }
}
