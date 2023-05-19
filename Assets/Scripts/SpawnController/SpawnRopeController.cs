using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using pancake.Rope2DEditor;
using Pancake.Threading.Tasks.Triggers;
using UnityEngine;

public class SpawnRopeController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPos;
    public GameObject Rope;
    private List<GameObject> ropes = new List<GameObject>();

    public void SpawnRope()
    {
        var newRopeLine = Instantiate(Rope, LevelController.Instance.currentLevel.transform).GetComponent<RopeMaker>();
        newRopeLine.gameObject.transform.position = SpawnPos.transform.position;
        List<DragEndPoint> endPoints = newRopeLine.GetComponentsInChildren<DragEndPoint>().ToList();
        if (endPoints[0].typeEnd == 1)
            endPoints[0].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end1);
        else endPoints[0].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end2);
        if (endPoints[1].typeEnd == 1)
            endPoints[1].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end1);
        else endPoints[1].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end2);
        newRopeLine.Index = ropes.Count();
        if (ropes.Count == 0 || ropes[newRopeLine.Index - 1].GetComponentInChildren<RopeMaker>().moved)
            MoveRope(newRopeLine.gameObject);
        ropes.Add(newRopeLine.gameObject);
        newRopeLine.movedCallBack += HandleRopeMoved;
    }

    void HandleRopeMoved(int index)
    {
        if (index + 1 < ropes.Count)
        {
            MoveRope(ropes[index + 1]);
        }
    }

    void MoveRope(GameObject gameObject)
    {
        gameObject.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0), 1.5f);
    }
}