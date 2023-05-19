using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using pancake.Rope2DEditor;
using Pancake.Threading.Tasks.Triggers;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.Serialization;

public class SpawnFanController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPos;
    public GameObject Fan;
    private List<GameObject> fans = new List<GameObject>();
    public void SpawnFan()
    {
        var newFan = Instantiate(Fan, LevelController.Instance.currentLevel.transform).GetComponentInChildren<DragFan>();
        newFan.transform.parent.transform.position = SpawnPos.transform.position;
        newFan.StartPos = SpawnPos.transform.position;
        newFan.Index = fans.Count();
        if(fans.Count == 0 || fans[newFan.Index - 1].GetComponentInChildren<DragFan>().moved) MoveFan((newFan.gameObject.transform.parent.gameObject));
        fans.Add(newFan.gameObject.transform.parent.gameObject);
        newFan.movedCallBack += HandleFanMoved;
    }

    void HandleFanMoved(int index)
    {
        if (index + 1 < fans.Count)
        {
            MoveFan(fans[index + 1]);
        }
    }

    void MoveFan(GameObject gameObject)
    {
        gameObject.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0), 1.5f);
    }
}