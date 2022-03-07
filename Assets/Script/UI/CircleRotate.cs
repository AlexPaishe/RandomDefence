using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleRotate : MonoBehaviour
{
    [SerializeField] private Transform _transCircle;
    [SerializeField] private Transform _trans;
    private Vector3 _mousePos;
    private Quaternion r;

    public void OnHandDrag()
    {
        _mousePos = Input.mousePosition;
        Vector2 dir = _mousePos - _trans.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        angle = (angle <= 0) ? (360 + angle) : angle;
        r = Quaternion.AngleAxis(angle, Vector3.forward);
        _trans.rotation = r;
        float z = r.z;
        float w = r.w;
        Debug.Log(r);
    }

    public void OnHandDrop()
    {
        float z = r.z;
        float w = r.w;
        if(z < 0.4 && z > -0.4 && w > 0.9 || z < 0.4 && z > -0.4 && w < -0.9)
        {
            _transCircle.localEulerAngles = Vector3.zero;
        }
        else if(z > 0.4 && z < 0.9 && w > 0.4 && w < 0.9||
            z > 0)
        {

        }
    }
}
