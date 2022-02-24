using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpTower : MonoCache
{
    public bool go = false;
    [SerializeField] private Transform _point;
    [SerializeField] private float gain = 0.9f;
    [SerializeField] private float longFace = 2.7f;
    private GameObject middleRing;

    private void Awake()
    {
        middleRing = GameObject.Find("Medium");
    }

    void Update()
    {
        if (gameObject.activeSelf)
        {
            Collider[] box = null;
            if (transform.parent.gameObject.GetComponentInParent<Cell>().Disk != 1)
            {
                int sectorType = transform.parent.gameObject.GetComponentInParent<Cell>().Type;
                Cell[] cells = middleRing.GetComponentsInChildren<Cell>();
                for (int i = 0; i < cells.Length; i++)
                {
                    if (sectorType == cells[i].Type)
                    {
                        box = Physics.OverlapBox(cells[i].transform.GetChild(0).position, new Vector3(0.5f, longFace, 1), Quaternion.identity);
                    }
                }
            }
            else
            {
                box = Physics.OverlapBox(transform.position, new Vector3(0.5f, longFace, 1), Quaternion.identity);
            }
            foreach (var item in box)
            {
                if (item.gameObject.TryGetComponent(out TowerSearchEnemy searchEnemy))
                {
                    float shortTimer = searchEnemy.Timer * gain;
                    if (searchEnemy.Timer == searchEnemy.GetMaxTimer())
                    {
                        searchEnemy.Timer = shortTimer;
                    }
                }

            }

        }

    }
}
