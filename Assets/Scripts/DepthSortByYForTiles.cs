using System.Collections;
using System.Collections.Generic;
using UnityEngine.Tilemaps;
using UnityEngine;



[ExecuteInEditMode]
[RequireComponent(typeof(Renderer))]

public class DepthSortByYForTiles : MonoBehaviour
{
    private const int IsometricRangePerYUnit = 100;

    [Tooltip("Use this to offset the object slightly in front or behind the Target object")]
    public int TargetOffset = 0;

    // Update is called once per frame
    void Update()
    {
        Tile[] renderers = GetComponents<Tile>();
        //foreach (Renderer renderer in renderers)
        //{
        //    renderer.sortingOrder = -(int)(renderer.transform.position.y * IsometricRangePerYUnit);
        //}
    }
}
