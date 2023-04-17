using System;
using System.Collections;
using System.Collections.Generic;
using pancake.Rope2DEditor;
using UnityEngine;

public class DragEndPoint : MonoBehaviour
{
    private Camera _camera;

    [SerializeField] private RopeMaker ropeMaker;
    [SerializeField] private LayerMask circleLayerMask;
    [SerializeField] private CircleCollider2D circleCollider2D;
    // Start is called before the first frame update
    public int typeEnd;
    public Vector3 StartPos;
    private bool hooked = false;
    void Start()
    {
        _camera = ropeMaker.GetComponentInParent<Level>().Camera;
        hooked = false;
        ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        var newLocalPos = typeEnd == 1 ? ropeMaker.end1 : ropeMaker.end2;
        newLocalPos.z = -1;
        transform.localPosition = newLocalPos;
        if (hooked == true) ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = true;
        else ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;
        if (Vector3.Distance(ropeMaker.end1,ropeMaker.end2)>=3) ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseDown()
    {
        if (typeEnd == 1)
        {
            StartPos = ropeMaker.transform.TransformPoint(ropeMaker.end1);
        }
        else
        {
            StartPos = ropeMaker.transform.TransformPoint(ropeMaker.end2);
        }
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        if (typeEnd == 1)
        {
            ropeMaker.end1 = ropeMaker.transform.InverseTransformPoint((worldPosition));
        }
        else
        {
            ropeMaker.end2 = ropeMaker.transform.InverseTransformPoint((worldPosition));
        }

        ropeMaker.CreateRope();
        ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        RaycastHit2D hit = Physics2D.CircleCast(worldPosition, circleCollider2D.radius, Vector3.forward,10f, circleLayerMask);
        if (hit)
        {
            bool circleHasHooked = hit.collider.gameObject.GetComponent<Anchor>().hasHooked;
            if (!circleHasHooked)
            {
                if (typeEnd == 1)
                {
                    ropeMaker.end1 =
                        ropeMaker.transform.InverseTransformPoint(hit.collider.gameObject.transform.position);
                }
                else
                {
                    ropeMaker.end2 =
                        ropeMaker.transform.InverseTransformPoint(hit.collider.gameObject.transform.position);
                }

                hooked = true;
                ropeMaker.CreateRope();
                hit.collider.gameObject.GetComponent<Anchor>().hasHooked = true;
            }
            else
            {
                if (typeEnd == 1)
                {
                    ropeMaker.end1 = ropeMaker.transform.InverseTransformPoint(StartPos);
                }
                else
                {
                    ropeMaker.end2 = ropeMaker.transform.InverseTransformPoint(StartPos);
                }
                ropeMaker.CreateRope();
            }
        }
        else
        {
            if (typeEnd == 1)
            {
                ropeMaker.end1 = ropeMaker.transform.InverseTransformPoint(StartPos);
            }
            else
            {
                ropeMaker.end2 = ropeMaker.transform.InverseTransformPoint(StartPos);
            }
            ropeMaker.CreateRope();
        }
        
    }
}