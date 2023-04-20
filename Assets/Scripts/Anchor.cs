using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasHooked = false;
    [SerializeField] private LayerMask anchorLayerMask;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
        RaycastHit2D[] hit = Physics2D.CircleCastAll(transform.position, GetComponent<CircleCollider2D>().radius*transform.localScale.x,
            -Vector3.forward, 10f,anchorLayerMask);
        Debug.DrawRay(transform.position,-Vector3.forward,Color.red);
        
        if (hit.Length == 0)
        {
            hasHooked = false;
        }
        else
        {
            if(!hit.Any(col => col.collider.gameObject.CompareTag("EndPoint")))  hasHooked = false;
        }


    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, GetComponent<CircleCollider2D>().radius*transform.localScale.x);
    }
}
