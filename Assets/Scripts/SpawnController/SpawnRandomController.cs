using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using pancake.Rope2DEditor;
public class SpawnRandomController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> Items;
    [SerializeField] private Transform SpawnPos;
    // Update is called once per frame
    public void SpawnRandom()
    {
        int pos = Random.Range(0, 3);
        GameObject item = Items[pos];
        switch (pos)
        {
            case 0:
                var newRopeLine = Instantiate(item, LevelController.Instance.currentLevel.transform);
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
                break;
            case 1:
                var newFan = Instantiate(item, LevelController.Instance.currentLevel.transform);
                newFan.transform.position = SpawnPos.transform.position;
                newFan.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),1.5f).OnComplete(
                    () =>
                    {
                        newFan.GetComponentInChildren<DragFan>().StartPos = SpawnPos.transform.position;
                    });
                break;
            case 2:
                var newGun = Instantiate(item, LevelController.Instance.currentLevel.transform);
                newGun.transform.position = SpawnPos.transform.position;
                newGun.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),1.5f).OnComplete(
                    () =>
                    {
                        newGun.GetComponentInChildren<GunEmplacement>().StartPos = SpawnPos.transform.position;
                    });
                break;
        }
    }
}
