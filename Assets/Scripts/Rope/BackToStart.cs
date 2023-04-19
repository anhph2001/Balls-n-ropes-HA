using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;


public class BackToStart : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 StartPoint;
    private Vector3 step;
    private float speed = .7f;
    void Start()
    {
        StartPoint = this.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 position = Vector3.MoveTowards(transform.position, StartPoint, speed * Time.deltaTime);
        transform.position = position;
    }
}
