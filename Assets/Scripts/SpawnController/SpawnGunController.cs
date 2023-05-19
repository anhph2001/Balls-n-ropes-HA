using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using pancake.Rope2DEditor;
using Pancake.Threading.Tasks.Triggers;
using PlayFab.ClientModels;
using UnityEngine;

public class SpawnGunController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPos;
    public GameObject Gun;
    private List<GameObject> guns = new List<GameObject>();
    public void SpawnGun()
    {
        var newGun = Instantiate(Gun, LevelController.Instance.currentLevel.transform).GetComponent<GunEmplacement>();
        newGun.transform.position = SpawnPos.transform.position;
        newGun.StartPos = SpawnPos.transform.position;
        newGun.Index = guns.Count();
        if(guns.Count == 0 || guns[newGun.Index - 1].GetComponent<GunEmplacement>().moved) MoveGun((newGun.gameObject));
        guns.Add(newGun.gameObject);
        newGun.movedCallBack += HandleGunMoved;
    }

    void HandleGunMoved(int index)
    {
        if (index + 1 < guns.Count)
        {
            MoveGun(guns[index + 1]);
        }
    }

    void MoveGun(GameObject gameObject)
    {
        gameObject.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0), 1.5f);
    }
}
