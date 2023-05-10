using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using pancake.Rope2DEditor;
using Pancake.Threading.Tasks.Triggers;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnFanController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPos;
    public GameObject Fan;

    public void SpawnFan()
    {
        //Instantiate(Rope, SpawnPos.transform.position,Quaternion.identity);
        var newFan = Instantiate(Fan, LevelController.Instance.currentLevel.transform);
        newFan.transform.position = SpawnPos.transform.position;
        newFan.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),1.5f).OnComplete(
            () =>
            {
                newFan.GetComponentInChildren<DragFan>().StartPos = SpawnPos.transform.position;
            });
    }
}