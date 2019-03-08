using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

	public float speed;
	public Rigidbody2D rb;
    private Vector2 direction;

    void Start() 
 	{
        rb = GetComponent<Rigidbody2D>();
	}

    private void FixedUpdate()
    {
        rb.velocity = direction * speed;

    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
  
    }

    public void Initialize(Vector2 direction)
    {
        this.direction = direction;
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Equals("Player"))
        {
            Destroy(gameObject);
            Debug.Log(collision.name);
        }
    }
}
