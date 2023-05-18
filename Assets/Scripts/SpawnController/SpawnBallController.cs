using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SpawnBallController : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject Ball;
    [SerializeField] private Transform SpawnPos;

    private List<GameObject> _balls = new List<GameObject>();
    // Update is called once per frame
    public void SpawnBall()
    {
        var newBall = Instantiate(Ball, LevelController.Instance.currentLevel.transform);
        newBall.layer = LayerMask.NameToLayer("BallHitPipe");
        newBall.transform.position = SpawnPos.transform.position; 
        _balls.Add(newBall);
    }

    public void ResetBall()
    {
        if (_balls.Count == 0) return;
        for (int i = 0; i < _balls.Count; i++)
        {
            //_balls[i].GetComponent<TrailRenderer>().enabled = false;
            _balls[i].SetActive(false);
        }

        StartCoroutine(DropBall());
    }

    IEnumerator DropBall()
    {
        int pos = 0;
        while (pos < _balls.Count)
        {
            yield return new WaitForSeconds(0.1f);
            _balls[pos].transform.position = SpawnPos.position;
            _balls[pos].layer = LayerMask.NameToLayer("BallHitPipe");
            _balls[pos].SetActive(true);
            pos++;
        }
    }
}
