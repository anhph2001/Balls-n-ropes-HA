using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;

public class PropellerFan : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [Range(1f, 100f)] private float force = 20f;
    private Rigidbody2D rb;
    [SerializeField] private GameObject fan;
    private Sequence _sequence;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            Rigidbody2D rbBall = other.gameObject.GetComponent<Rigidbody2D>();
            rbBall.AddForce(transform.TransformDirection(Vector3.up) * force, ForceMode2D.Impulse);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball"))
        { 
            LevelController.Instance.currentLevel.currentPoint += LevelController.Instance.currentLevel.pointWhenHit;
            GameObject point = PointPooling.instance.GetObjectPoint();
            if (!point) return;
            point.SetActive(true);
            point.transform.position = other.transform.position;
            _sequence = DOTween.Sequence();
            Vector3 endPos = new Vector3(point.transform.position.x, point.transform.position.y + 1f, 0);
            _sequence.Append(point.transform.DOMove(endPos, .8f))
            .Join(point.GetComponent<TextMeshPro>().DOFade(0, .8f)).OnComplete(() =>
            {
                point.SetActive(false);
                float newAlpha = 1f;
                Color newColor = point.GetComponent<TextMeshPro>().color;
                newColor.a = newAlpha;
                point.GetComponent<TextMeshPro>().color = newColor;
            });
            _sequence.Play();
        }
    }

} 