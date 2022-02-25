using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameFire : MonoBehaviour
{
    public float damage;
    private int _level;

    private void OnTriggerStay(Collider other)
    {
        if(other.GetComponent<EnemyHealth>())
        {
            other.GetComponent<EnemyHealth>().Health -= damage;
        }
    }

    /// <summary>
    /// ���������� ��������
    /// </summary>
    public void Upgrade()
    {
        _level++;
        if(_level == 3)
        {
            _level = 2;
        }
        damage = Base.CF[_level];
    }

    /// <summary>
    /// ���������� ���������� � ������� ����������
    /// </summary>
    public void Downgrade()
    {
        _level = 0;
        damage = Base.CF[_level];
    }
}
