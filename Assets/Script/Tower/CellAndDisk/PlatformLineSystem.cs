using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLineSystem : MonoBehaviour
{
    [SerializeField] private PlatformLine[] _line;
    private int[] _arrayLine = new int[6] { 0, 0, 1, 1, 2, 2 };

    private void Awake()
    {
        for (int i = _arrayLine.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i);
            int tmp = _arrayLine[i];
            _arrayLine[i] = _arrayLine[j];
            _arrayLine[j] = tmp;
        }

        for(int i = 0; i < _arrayLine.Length; i++)
        {
            _line[i].Type = _arrayLine[i];
        }
    }
}
