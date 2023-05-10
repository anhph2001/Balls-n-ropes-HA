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

    public void SpawnRope()
    {
        //Instantiate(Rope, SpawnPos.transform.position,Quaternion.identity);
        var newRopeLine = Instantiate(Rope, LevelController.Instance.currentLevel.transform);
        newRopeLine.transform.position = SpawnPos.transform.position;
        newRopeLine.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),1.5f).OnComplete(
            () =>
            {
                List<DragEndPoint> endPoints = newRopeLine.GetComponentsInChildren<DragEndPoint>().ToList();
                RopeMaker rm = newRopeLine.GetComponentInChildren<RopeMaker>();
                if (endPoints[0].typeEnd == 1)
                    endPoints[0].StartPos = rm.transform.TransformPoint(rm.end1);
                else endPoints[0].StartPos = rm.transform.TransformPoint(rm.end2);
                
                if (endPoints[1].typeEnd == 1)
                    endPoints[1].StartPos = rm.transform.TransformPoint(rm.end1);
                else endPoints[1].StartPos = rm.transform.TransformPoint(rm.end2);
            });
    }
}