using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLineSystem : MonoBehaviour
{
    [SerializeField] private PlatformCell[] _cells;
    private int[] _arrayLineNear = new int[6] { 0, 0, 1, 1, 2, 2 };
    private int[] _arrayLineMedium = new int[6] { 0, 0, 1, 1, 2, 2 };
    private int[] _arrayLineFaraway = new int[6] { 0, 0, 1, 1, 2, 2 };

    private void Start()
    {
        for (int i = _arrayLineNear.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i);
            int tmp = _arrayLineNear[i];
            _arrayLineNear[i] = _arrayLineNear[j];
            _arrayLineNear[j] = tmp;
        }

        for (int i = _arrayLineMedium.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i);
            int tmp = _arrayLineMedium[i];
            _arrayLineMedium[i] = _arrayLineMedium[j];
            _arrayLineMedium[j] = tmp;
        }

        for (int i = _arrayLineFaraway.Length - 1; i >= 0; i--)
        {
            int j = Random.Range(0, i);
            int tmp = _arrayLineFaraway[i];
            _arrayLineFaraway[i] = _arrayLineFaraway[j];
            _arrayLineFaraway[j] = tmp;
        }

        for (int i = 0; i < _cells.Length; i++)
        {
            if (i < 6)
            {
                _cells[i].Type = _arrayLineNear[i];
            }
            else if(i > 5 && i < 12)
            {
                _cells[i].Type = _arrayLineMedium[i - 6];
            }
            else if(i > 11)
            {
                _cells[i].Type = _arrayLineFaraway[i - 12];
            }
        }
    }
}
