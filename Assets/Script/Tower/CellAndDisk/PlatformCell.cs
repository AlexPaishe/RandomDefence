using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformCell : MonoCache
{
    [SerializeField] private MeshRenderer _mesh;
    [SerializeField] private int _disk;
    private PlatformLine _platformLine;
    private TowerDefence _tower;
    private void OnTriggerEnter(Collider other)
    {
        if(other.GetComponent<PlatformLine>())
        {
            _platformLine = other.GetComponent<PlatformLine>();

           switch(_platformLine.Type)
           {
                case 0: _mesh.material.SetColor("_MainColor", Color.red); break;
                case 1: _mesh.material.SetColor("_MainColor", Color.green); break;
                case 2: _mesh.material.SetColor("_MainColor", Color.blue); break;
           }
        }
        if(other.GetComponent<TowerDefence>())
        {
            switch(other.GetComponent<TowerDefence>().Type)
            {
                case 0: _mesh.material.SetColor("_ColorEmission", Color.red); break;
                case 1: _mesh.material.SetColor("_ColorEmission", Color.green); break;
                case 2: _mesh.material.SetColor("_ColorEmission", Color.blue); break;
            }
            _tower = other.GetComponent<TowerDefence>();
        }
    }

    public override void OnTick()
    {
        if (_tower != null)
        {
            if (_tower.gameObject.activeSelf == false)
            {
                _tower = null;
                _mesh.material.SetColor("_ColorEmission", Color.black);
            }
        }
    }
}
