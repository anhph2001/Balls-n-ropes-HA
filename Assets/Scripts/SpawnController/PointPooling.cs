using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PointPooling : MonoBehaviour
{
    public static PointPooling instance;
    public List<GameObject> poolObjects = new List<GameObject>();
    [SerializeField] private GameObject point;
    [SerializeField] private int count;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    void Start()
    {
        for (int i = 0; i < count; i++)
        {
            GameObject pointHit = Instantiate(point,this.gameObject.transform);
            pointHit.GetComponent<TextMeshProUGUI>().text = "$" + LevelController.Instance.currentLevel.pointWhenHit;
            pointHit.SetActive(false);
            poolObjects.Add(pointHit);
        }
    } 
    
    public GameObject GetObjectPoint()
    {
        for (int i = 0; i < count; i++)
        {
            if (!poolObjects[i].activeInHierarchy)
                return poolObjects[i];
        }

        return null;
    }
}
