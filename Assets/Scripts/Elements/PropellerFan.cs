using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PropellerFan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Range(1f, 100f)] private float force = 20f;
    private Rigidbody2D rb;
    [SerializeField] private GameObject fan;

    private void Start()
    {
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
} 