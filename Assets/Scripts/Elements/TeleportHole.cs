using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class TeleportHole : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Transform TeleportExit;
    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            Vector3 exitPosition = TeleportExit.position;
            TrailRenderer trailRenderer = col.gameObject.GetComponent<TrailRenderer>();
            trailRenderer.enabled = false;
            col.gameObject.transform.position = exitPosition;
            DOTween.Sequence().AppendInterval(.1f).AppendCallback(() =>
                trailRenderer.enabled = true);
            Rigidbody2D ballRigidbody = col.gameObject.GetComponent<Rigidbody2D>();
            ballRigidbody.velocity = -ballRigidbody.velocity;
        }
    }
}
