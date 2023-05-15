using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class GunEmplacement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] [Range(1f, 100f)] private float force = 45f;
    public Vector3 StartPos;
    private Camera _camera;
    private bool Moving = true;
    [SerializeField]
    private LayerMask gunLayerMask;

    private ParticleSystem fxExplosive;

    private Anchor currentAnchor = null;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _camera = GetComponentInParent<Level>().Camera;
        fxExplosive = GetComponentInChildren<ParticleSystem>();
    }

    private void OnMouseDown()
    {
        StartPos = this.transform.position;
        GetComponent<CircleCollider2D>().enabled = false;
        Moving = true;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        GetComponent<CircleCollider2D>().enabled = true;
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        if (LevelController.Instance.currentLevel.LevelType == LevelType.HasAnchor)
        {
            RaycastHit2D hit = Physics2D.CircleCast(worldPosition, .3f, Vector3.forward, 10f, gunLayerMask);
            if (hit)
            {
                Vector3 anchorPos = hit.collider.gameObject.transform.position;
                if (!hit.collider.gameObject.GetComponent<Anchor>().hasHooked)
                {
                    transform.position = new Vector3(anchorPos.x, anchorPos.y, 0);
                    hit.collider.gameObject.GetComponent<Anchor>().hasHooked = true;
                    if (currentAnchor != null) currentAnchor.hasHooked = false;
                    currentAnchor = hit.collider.gameObject.GetComponent<Anchor>();
                }
                else transform.position = StartPos;

            }
            else
            {
                transform.position = StartPos;
            }
        }
        else
        {
            transform.position = worldPosition;
        }
        Moving = false;
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball") && !Moving)
        {
            LevelController.Instance.currentLevel.currentPoint += LevelController.Instance.currentLevel.pointWhenHit;
            Vector3 ballPos = col.gameObject.transform.position;
            Vector2 aimDirection = new Vector2(ballPos.x,ballPos.y)  - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
            
            Rigidbody2D rbBall = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = col.gameObject.transform.position - transform.position;
            rbBall.AddForce(direction*force*(Mathf.Abs(rbBall.velocity.y/13f)),ForceMode2D.Impulse);
            fxExplosive.Play();
            DOTween.Sequence().AppendInterval(.1f).AppendCallback(() =>
            {
            fxExplosive.Stop();
            });
        }
    }


}
