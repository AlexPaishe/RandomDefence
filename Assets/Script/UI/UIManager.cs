using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Image[] _icons;
    [SerializeField] private Sprite[] _spritesIcon;
    [SerializeField] private UpgradeSpawn _upgrade;
    [SerializeField] private MoneySystem _money;
    [SerializeField] private ColorDisk _disk;

    /// <summary>
    /// ���������� ��������� ������ �� ���� ������ �� ����
    /// </summary>
    public void DiskUp()
    {
        Base.Disk++;
        if(Base.Disk > 2)
        {
            Base.Disk = 2;
        }
        _disk.Disk = Base.Disk;
    }

    /// <summary>
    /// ���������� ��������� ������ �� ���� ����� �� ����
    /// </summary>
    public void DiskDown()
    {
        Base.Disk--;
        if (Base.Disk < 0)
        {
            Base.Disk = 0;
        }
        _disk.Disk = Base.Disk;
    }

    /// <summary>
    /// ���������� ������ ��������
    /// </summary>
    public void Upgrade()
    {
        if (_money.UpgradeBuild())
        {
            _upgrade.SpawnUpgrade();
        }
    }
}
