using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IgnoreCollision : MonoBehaviour
{
    [SerializeField]
    public Collider2D other;

    private void Awake()
    {
        Physics2D.IgnoreCollision(GetComponent<CircleCollider2D>(), other, true);
    }

}
