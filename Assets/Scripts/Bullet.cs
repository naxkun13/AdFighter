using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed = 20f;
	public Rigidbody2D rb;

    void Start() 
 	{
        rb = GetComponent<Rigidbody2D>();
        rb.AddRelativeForce(Vector2.right * speed, ForceMode2D.Impulse);
	}

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }


}
