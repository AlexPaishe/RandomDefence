using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Eggs : MonoCache
{
    public Vector3[] point;
    [SerializeField] private float _speed;
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private Transform _trans;
    public int line;
    private float _t = 0;
    private int _type;
    private EnemySpawn _enemyS;
    public float T
    {
        get
        {
            return _t;
        }
        set
        {
            _t = value;
            if(_t >= 1)
            {
                switch(_type)
                {
                    case 0: Destroy(gameObject); break;
                    case 1:
                        int rand = Random.Range(1, 4);
                        _enemyS.SpawnEggsEnemy(rand, line, transform.position);
                        Destroy(gameObject); break;
                    case 2:
                        _enemyS.SpawnEggsEnemy(0, line, transform.position);
                        Destroy(gameObject); break;
                }
            }
        }
    }

    /// <summary>
    /// Раскраска яйца
    /// </summary>
    /// <param name="var"></param>
    public void TypeEgg(int var)
    {
        switch(var)
        {
            case 0: _mesh.material.SetColor("_EmissionColor", Color.red); break;
            case 1: _mesh.material.SetColor("_EmissionColor", Color.green); break;
            case 2: _mesh.material.SetColor("_EmissionColor", Color.blue); break;
        }
        _type = var;
    }

    public override void OnTick()
    {
        _trans.position = Bezier.GetPoint(point[0], point[1], point[2], point[3], T);
        T += Time.deltaTime * _speed;
        transform.rotation = Quaternion.LookRotation(Bezier.GetFirstDerivative(point[0], point[1], point[2], point[3], T));
    }

    private void Start()
    {
        _enemyS = FindObjectOfType<EnemySpawn>();
    }
}
