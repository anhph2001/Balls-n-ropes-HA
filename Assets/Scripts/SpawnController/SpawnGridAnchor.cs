using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Pancake;
using UnityEngine;

public class SpawnGridAnchor : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private int columns;
    [SerializeField] private int rows;
    [SerializeField] private GameObject gridObject;
    [SerializeField] private float objectSize = 1f;

    // Update is called once per frame
    void GenerateGrid()
    {
        float gridWidth = columns * objectSize;   
        float gridHeight = rows * objectSize;
        Vector3 gridStartPosition = new Vector3(-gridWidth / 2f + objectSize / 2f, gridHeight / 2f - objectSize / 2f, 0f);
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                GameObject gridObjInstance = Instantiate(gridObject, transform);
                
                float posX = gridStartPosition.x + column * objectSize;
                float posY = gridStartPosition.y - row * objectSize;
                gridObjInstance.transform.position = new Vector3(posX, posY, 0);
            }
        }
    }
#if UNITY_EDITOR
    [Button]
    public void GenGrid()
    {
        GenerateGrid();
    }

    [Button]
    public void ClearGrid()
    {
        var childs = transform.childCount;
        for (int i = childs - 1; i >= 0; i--)
        {
            DestroyImmediate(transform.GetChild(i).gameObject, true);
        }
    }
#endif
}
