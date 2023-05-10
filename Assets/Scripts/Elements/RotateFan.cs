using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateFan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject fan;
    private Camera _camera;
    private float angleOffset;
    private Vector3 screenPos;
    
    // Update is called once per frame
    void Start()
    {
        _camera = GetComponentInParent<Level>().Camera;
    }

    private void OnMouseDown()
    {
        screenPos = _camera.WorldToScreenPoint(transform.parent.position);
        Vector3 v3 = Input.mousePosition - screenPos;
        angleOffset = (Mathf.Atan2(transform.parent.right.y, transform.parent.right.x) - Mathf.Atan2(v3.y, v3.x))  * Mathf.Rad2Deg;
    }

    private void OnMouseDrag()
    {
        Vector3 v3 = Input.mousePosition - screenPos;
        float angle = Mathf.Atan2(v3.y, v3.x) * Mathf.Rad2Deg;
        transform.parent.eulerAngles = new Vector3(0, 0, angle + angleOffset);
    }
}

