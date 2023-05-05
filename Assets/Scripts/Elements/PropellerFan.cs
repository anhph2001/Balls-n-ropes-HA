using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerFan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Range(1f, 100f)] private float force = 20f;
    private Camera _camera;
    private Rigidbody2D rb;

    private void Start()
    {
        _camera = GetComponentInParent<Level>().Camera;
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D rbBall = other.gameObject.GetComponent<Rigidbody2D>();
            rbBall.AddForce(transform.TransformDirection(Vector3.up)*force,ForceMode2D.Impulse);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        transform.position = worldPosition;
    }
} 