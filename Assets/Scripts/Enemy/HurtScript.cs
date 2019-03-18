using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HurtScript : MonoBehaviour {
    public int Health = 10;
    private PlayerController thePlayerStats;
    public int expToGive;

	// Use this for initialization
	void Start () {
        thePlayerStats = FindObjectOfType<PlayerController>();
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag.Equals("Bullet")) {
            //Destroy(collision.gameObject);
            Health -= 5;
            if (Health <= 0) {
                Destroy(gameObject);
                thePlayerStats.AddExp( expToGive );
            }
        }
    }
}
