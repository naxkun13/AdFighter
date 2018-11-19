using System.Collections;
using UnityEngine;

public class PlayerController : Character {
	[SerializeField]
	private Stat health;

	private float initHealth = 100;

	protected override void Start()
	{
		health.Initialize (initHealth, initHealth);

		base.Start();

	}
	protected override void Update()
	{
		GetInput ();
//		health.MyCurrentValue = 100;

		base.Update ();
	}


	private void GetInput()
	{


		direction = Vector2.zero;


		if (Input.GetKeyDown (KeyCode.I)) 
		{
			health.MyCurrentValue -= 10;
		}
		if (Input.GetKeyDown (KeyCode.O)) 
		{
			health.MyCurrentValue += 10;
		}



		if (Input.GetKey (KeyCode.W)) 
		{
			direction += Vector2.up;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			direction += Vector2.right;
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			direction += Vector2.left;
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			direction += Vector2.down;
		}
	}
}