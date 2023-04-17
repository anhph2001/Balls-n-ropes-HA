using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    // Start is called before the first frame update
    public bool hasHooked = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
       
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        hasHooked = true;
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        hasHooked = false;
    }
}
