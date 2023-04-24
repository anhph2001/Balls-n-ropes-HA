using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Anchor : MonoBehaviour
{
    public bool hasHooked = false;
    [SerializeField] private LayerMask anchorLayerMask;
}
