using System;
using System.Collections;
using System.Collections.Generic;
using Pancake;
using pancake.Rope2DEditor;
using UnityEngine;
public class Ball : MonoBehaviour
{
    // Start is called before the first frame update
    private float TimeCount = .5f;
    private Vector3 prePos;
    public Vector3 direction;
    public float force = 50f;
    private Transform spawnPos;
    
    void Start()
    {
        prePos = transform.position;
        spawnPos = LevelController.Instance.currentLevel.SpawnBallPos;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimeCount -= Time.deltaTime;
        if (TimeCount <= 0)
        {
            TimeCount = .5f;
            prePos = this.transform.position;
        }
    }

    private void Update()
    {
        if (transform.position.y < -4f)
        {
            GetComponent<TrailRenderer>().enabled = false;
            transform.position = spawnPos.position;
            GetComponent<Rigidbody2D>().Sleep();
        }

        if (transform.position.y > -4f && transform.position.y < 9) 
        {
            GetComponent<TrailRenderer>().enabled = true;
        }
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Ground"))
        {
            LevelController.Instance.currentLevel.currentPoint += LevelController.Instance.currentLevel.pointWhenHit;
            direction = (transform.position - prePos).normalized;
            Debug.DrawRay(transform.position , direction * 1f, Color.yellow);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, (transform.position - prePos).normalized,1f);
            for (int i = 0; i < hits.Length; i++)
            { ;
                RaycastHit2D hit = hits[i];
                if (hit.collider.gameObject.CompareTag("Rope"))
                {
                    GameObject rope = hit.collider.gameObject;
                    RopeMaker rm = rope.GetComponentInParent<RopeMaker>();
                    if (rm.ground.GetComponent<BoxCollider2D>().enabled)
                    rope.GetComponent<Rigidbody2D>().AddForce(direction*force,ForceMode2D.Impulse);
                    
                }
            }
        }
    }
    
}
