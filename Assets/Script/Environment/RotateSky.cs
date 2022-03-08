using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateSky : MonoCache
{
    [SerializeField] private float _rotateSky;
    private float _rotate;

    public override void OnTick()
    {
        _rotate += Time.deltaTime * _rotateSky;
        RenderSettings.skybox.SetFloat("_Rotation", _rotate);
    }
}
