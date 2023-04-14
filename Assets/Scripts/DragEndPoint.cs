using System;
using System.Collections;
using System.Collections.Generic;
using pancake.Rope2DEditor;
using UnityEngine;
public class DragEndPoint : MonoBehaviour
{
    private Camera _camera;
    [SerializeField] private RopeMaker ropeMaker;
    // Start is called before the first frame update
    public int typeEnd;
    void Start()
    {
        _camera = ropeMaker.GetComponentInParent<Level>().Camera;
    }

    // Update is called once per frame
    void Update()
    {
        var newLocalPos = typeEnd == 1? ropeMaker.end1 : ropeMaker.end2;
        newLocalPos.z = -1;
        transform.localPosition = newLocalPos;
        Collider[] colliders = Physics.OverlapSphere(transform.position, 1f);
        Debug.Log(colliders.Length);
        foreach (Collider hit in colliders)
            Debug.Log(hit.gameObject);
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        
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

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(other.gameObject);
    }
}