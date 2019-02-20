﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]

public class DepthSortByY : MonoBehaviour {

    private const int IsometricRangePerYUnit = 100;

    [Tooltip("Will use this object to compute z-order")]
    public Transform Target;

    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public int TargetOffset = 0;

    // Update is called once per frame
    void Update () {
        if (Target == null)
            Target = transform;
        Renderer renderer = GetComponent< Renderer >();
        renderer.sortingOrder = -(int)(transform.position.y * IsometricRangePerYUnit);
    }
}
