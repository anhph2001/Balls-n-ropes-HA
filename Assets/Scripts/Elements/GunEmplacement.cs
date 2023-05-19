using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
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

    private Sequence _sequence;

    private ParticleSystem fxExplosive;

    private Anchor currentAnchor = null;
    public bool moved = false;
    public Action<int> movedCallBack;
    public int Index;
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
                    if (!moved)
                    {
                        movedCallBack?.Invoke(Index);
                        moved = true;
                    }
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

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ball") && !Moving)
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
            Vector3 ballPos = other.gameObject.transform.position;
            Vector2 aimDirection = new Vector2(ballPos.x,ballPos.y)  - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
            
            Rigidbody2D rbBall = other.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = other.gameObject.transform.position - transform.position;
            rbBall.AddForce(direction*force,ForceMode2D.Impulse);
            fxExplosive.Play();
            DOTween.Sequence().AppendInterval(.1f).AppendCallback(() =>
            {
                fxExplosive.Stop();
            });
        }
        
    }
}
