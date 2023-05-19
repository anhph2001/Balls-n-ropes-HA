using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using UnityEngine;
using pancake.Rope2DEditor;
using PlayFab.Internal;

public class SpawnRandomController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private List<GameObject> Items;

    [SerializeField] private Transform SpawnPos;

    // Update is called once per frame
    public List<GameObject> randomItems = new List<GameObject>();
    public List<int> types = new List<int>();
    public void SpawnRandom()
    {
        int pos = Random.Range(0, 3);
        GameObject item = Items[pos];
        switch (pos)
        {
            case 0:
                var newRopeLine = Instantiate(item, LevelController.Instance.currentLevel.transform)
                    .GetComponent<RopeMaker>();
                newRopeLine.gameObject.transform.position = SpawnPos.transform.position;
                List<DragEndPoint> endPoints = newRopeLine.GetComponentsInChildren<DragEndPoint>().ToList();
                if (endPoints[0].typeEnd == 1)
                    endPoints[0].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end1);
                else endPoints[0].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end2);
                if (endPoints[1].typeEnd == 1)
                    endPoints[1].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end1);
                else endPoints[1].StartPos = newRopeLine.transform.TransformPoint(newRopeLine.end2);
                newRopeLine.Index = randomItems.Count();
                if (randomItems.Count() == 0 ||
                    GetBoolValueMoved(randomItems[newRopeLine.Index - 1], types[newRopeLine.Index - 1]))
                {
                    MoveItem(newRopeLine.gameObject);
                }

                randomItems.Add(newRopeLine.gameObject);
                types.Add(pos);
                newRopeLine.movedCallBack += HandleItemMoved;
                break;
            case 1:
                var newFan = Instantiate(item, LevelController.Instance.currentLevel.transform).GetComponentInChildren<DragFan>();
                newFan.transform.parent.transform.position = SpawnPos.transform.position;
                newFan.StartPos = SpawnPos.transform.position;
                newFan.Index = randomItems.Count();
                if(randomItems.Count == 0 || GetBoolValueMoved(randomItems[newFan.Index - 1], types[newFan.Index -1])) MoveItem((newFan.gameObject.transform.parent.gameObject));
                randomItems.Add(newFan.gameObject.transform.parent.gameObject);
                types.Add(pos);
                newFan.movedCallBack += HandleItemMoved;
                break;
            case 2:
                var newGun = Instantiate(item, LevelController.Instance.currentLevel.transform)
                    .GetComponent<GunEmplacement>();
                newGun.gameObject.transform.position = SpawnPos.transform.position;
                newGun.Index = randomItems.Count();
                if (randomItems.Count() == 0 ||
                    GetBoolValueMoved(randomItems[newGun.Index - 1], types[newGun.Index - 1]))
                {
                    MoveItem((newGun.gameObject));
                }
                    
                randomItems.Add(newGun.gameObject);
                types.Add(pos);
                newGun.movedCallBack += HandleItemMoved;
                break;
        }
    }

    void HandleItemMoved(int index)
    {
        if (index + 1 < randomItems.Count)
        {
            MoveItem(randomItems[index + 1]);
        }
    }

    void MoveItem(GameObject gameObject)
    {
        gameObject.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),
            1.5f);
    }

    private bool GetBoolValueMoved(GameObject _gameObject, int type)
    {
        if (type == 0) return _gameObject.GetComponent<RopeMaker>().moved;
        if (type == 1) return _gameObject.GetComponentInChildren<DragFan>().moved;
        if (type == 2) return _gameObject.GetComponent<GunEmplacement>().moved;
        return true;
    }


}
