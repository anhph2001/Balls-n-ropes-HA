using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragFan : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 StartPos;
    private Camera _camera;
    [SerializeField] private LayerMask fanLayerMask;
    private Anchor currentAnchor = null;
    [SerializeField] private GameObject fan;
    void Start()
    {
        _camera = GetComponentInParent<Level>().Camera;
    }
    private void OnMouseDown()
    {
        StartPos = fan.transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        fan.transform.position = worldPosition;
    }

    private void OnMouseUp()
    {
        Vector3 mousePosition = Input.mousePosition;
        Vector3 worldPosition = _camera.ScreenToWorldPoint(mousePosition);
        worldPosition.z = 0;
        if (LevelController.Instance.currentLevel.LevelType == LevelType.HasAnchor)
        {
            RaycastHit2D hit = Physics2D.CircleCast(worldPosition, .3f, Vector3.forward, 10f, fanLayerMask);
            if (hit)
            {
                Vector3 anchorPos = hit.collider.gameObject.transform.position;
                if (!hit.collider.gameObject.GetComponent<Anchor>().hasHooked)
                {
                    fan.transform.position = new Vector3(anchorPos.x, anchorPos.y, 0);
                    hit.collider.gameObject.GetComponent<Anchor>().hasHooked = true;
                    if (currentAnchor != null) currentAnchor.hasHooked = false;
                    currentAnchor = hit.collider.gameObject.GetComponent<Anchor>();
                }
                else fan.transform.position = StartPos;

            }
            else
            {
                fan.transform.position = StartPos;
            }
        }
        else
        {
            fan.transform.position = worldPosition;
        }
    }
}
