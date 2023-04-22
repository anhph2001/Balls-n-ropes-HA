using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunEmplacement : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    [SerializeField] [Range(1f, 100f)] private float force = 45f;
    public Vector3 StartPos;
    private Camera _camera;

    [SerializeField]
    private LayerMask gunLayerMask;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        _camera = GetComponentInParent<Level>().Camera;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        StartPos = this.transform.position;
        GetComponent<CircleCollider2D>().enabled = false;
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
        RaycastHit2D hit = Physics2D.CircleCast(worldPosition, .3f, Vector3.forward,10f, gunLayerMask);
        if (hit)
        {
            Vector3 anchorPos = hit.collider.gameObject.transform.position;
            if (!hit.collider.gameObject.GetComponent<Anchor>().hasHooked)
            transform.position = new Vector3(anchorPos.x, anchorPos.y, 0);
            else transform.position = StartPos;
        }
        else
        {
            transform.position = StartPos;
        }
        
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.CompareTag("Ball"))
        {
            LevelController.Instance.currentLevel.currentPoint += LevelController.Instance.currentLevel.pointWhenHit;
            Vector3 ballPos = col.gameObject.transform.position;
            Vector2 aimDirection = new Vector2(ballPos.x,ballPos.y)  - rb.position;
            float aimAngle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg - 90f;
            rb.rotation = aimAngle;
            
            Rigidbody2D rbBall = col.gameObject.GetComponent<Rigidbody2D>();
            Vector3 direction = col.gameObject.transform.position - transform.position;
            rbBall.AddForce(direction*force*(Mathf.Abs(rbBall.velocity.y/13f)),ForceMode2D.Impulse);
            
        }
    }


}
