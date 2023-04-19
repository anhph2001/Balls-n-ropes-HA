using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnBallController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Ball;
    [SerializeField] private Transform SpawnPos;
    void Start()
    {
        
    }

    // Update is called once per frame
    public void SpawnBall()
    {
        var newBall = Instantiate(Ball, LevelController.Instance.currentLevel.transform);
        newBall.layer = LayerMask.NameToLayer("BallHitPipe");
        newBall.transform.position = SpawnPos.transform.position;

    }
}
