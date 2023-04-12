using System;
using System.Collections;
using System.Collections.Generic;
using pancake.Rope2DEditor;
using UnityEngine;
public class DragEndPoint : MonoBehaviour
{
    [SerializeField] private Camera camera;
    [SerializeField] private RopeMaker ropeMaker;
    // Start is called before the first frame update
    public int typeEnd;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var newLocalPos = typeEnd == 1? ropeMaker.end1 : ropeMaker.end2;
        newLocalPos.z = -1;
        transform.localPosition = newLocalPos;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = camera.ScreenToWorldPoint(mousePosition);
        
        worldPosition.z = 0;
        if(typeEnd == 1)
        { ropeMaker.end1 = ropeMaker.transform.InverseTransformPoint((worldPosition));
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
        ropeMaker.ground.GetComponent<BoxCollider2D>().enabled = true;
    }
}