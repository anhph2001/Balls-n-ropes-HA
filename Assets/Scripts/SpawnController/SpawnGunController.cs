using System.Collections;
using System.Collections.Generic;
using System.Linq;
using DG.Tweening;
using pancake.Rope2DEditor;
using Pancake.Threading.Tasks.Triggers;
using UnityEngine;

public class SpawnGunController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPos;
    public GameObject Gun;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnGun()
    {
        //Instantiate(Rope, SpawnPos.transform.position,Quaternion.identity);
        var newGun = Instantiate(Gun, LevelController.Instance.currentLevel.transform);
        newGun.transform.position = SpawnPos.transform.position;
        newGun.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),1.5f).OnComplete(
            () =>
            {
                newGun.GetComponent<GunEmplacement>().StartPos = SpawnPos.transform.position;
            });
    }
}
