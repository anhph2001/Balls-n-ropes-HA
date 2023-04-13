using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using Pancake.Threading.Tasks.Triggers;
using UnityEngine;

public class SpawnRopeController : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject SpawnPos;
    public GameObject Rope;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnRope()
    {
        //Instantiate(Rope, SpawnPos.transform.position,Quaternion.identity);
        var newRopeLine = Instantiate(Rope, LevelController.Instance.currentLevel.transform);
        newRopeLine.transform.position = SpawnPos.transform.position;
        newRopeLine.transform.DOMove(new Vector3(SpawnPos.transform.position.x, SpawnPos.transform.position.y + 4f, 0),1.5f);
    }
}
