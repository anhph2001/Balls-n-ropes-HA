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
    public int hooked = 0;
    private Anchor currentAnchor = null;
    void Start()
    {
        _camera = ropeMaker.GetComponentInParent<Level>().Camera;
        hooked = 0;
        ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;
        UpdateEndPoint();
    }

    // Update is called once per frame
    void UpdateEndPoint()
    {
        var newLocalPos = typeEnd == 1 ? ropeMaker.end1 : ropeMaker.end2;
        newLocalPos.z = -1;
        transform.localPosition = newLocalPos;
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

        UpdateEndPoint();
        ropeMaker.CreateRope();
        ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;
    }

    private void OnMouseUp()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        if (LevelController.Instance.currentLevel.LevelType == LevelType.HasAnchor)
        {
            RaycastHit2D hit = Physics2D.CircleCast(worldPosition, circleCollider2D.radius, Vector3.forward, 10f,
                circleLayerMask);
            if (hit)
            {
                bool circleHasHooked = hit.collider.gameObject.GetComponent<Anchor>().hasHooked;
                if (!circleHasHooked)
                {
                    if (typeEnd == 1)
                    {
                        Vector3 pos = hit.collider.gameObject.transform.position;
                        ropeMaker.end1 =
                            ropeMaker.transform.InverseTransformPoint(pos.x, pos.y, 0);
                    }
                    else
                    {
                        Vector3 pos = hit.collider.gameObject.transform.position;
                        ropeMaker.end2 =
                            ropeMaker.transform.InverseTransformPoint(pos.x, pos.y, 0);
                    }

                    hooked = 1;
                    ropeMaker.UpdateHookedCount();

                    ropeMaker.CreateRope();
                    hit.collider.gameObject.GetComponent<Anchor>().hasHooked = true;
                    if (currentAnchor != null) currentAnchor.hasHooked = false;
                    currentAnchor = hit.collider.gameObject.GetComponent<Anchor>();
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
        else
        {
            if (typeEnd == 1)
            {
                ropeMaker.end1 = ropeMaker.transform.InverseTransformPoint(worldPosition);
            }
            else
            {
                ropeMaker.end2 = ropeMaker.transform.InverseTransformPoint(worldPosition);
            }

            ropeMaker.CreateRope();
        }

        UpdateEndPoint();
        ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = true;
        if (ropeMaker.countHooked < 2 && LevelController.Instance.currentLevel.LevelType == LevelType.HasAnchor) ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false; ;
        if (ropeMaker.countHooked == 2) ropeMaker.movedCallBack?.Invoke(ropeMaker.Index);
        if (Vector3.Distance(ropeMaker.end1,ropeMaker.end2)>=3) ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = false;

    }
}