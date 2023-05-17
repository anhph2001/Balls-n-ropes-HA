using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxSpin : MonoBehaviour
{
    [SerializeField] [Range(20f,100f)] private float speedSpin = 80f;
    // Update is called once per frame
    void Update()
    {
        var rotation = Time.deltaTime * speedSpin;
        transform.Rotate(new Vector3(0, 0, rotation));
    }
}
