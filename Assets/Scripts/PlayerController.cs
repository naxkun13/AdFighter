using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Character {
	[SerializeField]
	private Stat health;

	[SerializeField]
	private Stat exp;

	[SerializeField]
	private Text levelText;


	private float initHealth = 100;


	protected override void Start()
	{
		health.Initialize (initHealth, initHealth);
		base.Start ();
	}

	protected override void Update()
	{
		
		GetInput ();
		GetLevel ();
		LevelUp();

		levelText.text = MyLevel.ToString ();

		base.Update ();
	}

	private void GetLevel()
	{
		exp.Initialize (exp.MyCurrentValue, 100 * MyLevel * Mathf.Pow (MyLevel, 0.4f));
	}

	private void LevelUp()
	{
		if (exp.MyCurrentValue == exp.MyMaxValue)
		{
			MyLevel += 1;
			exp.MyCurrentValue = 0;
		}
		
	}

	private void GetInput()
	{


		direction = Vector2.zero;

		//debug HP I = -10hp; O = +10hp.
		if (Input.GetKeyDown (KeyCode.I)) 
		{
			health.MyCurrentValue -= 10;
		}
		if (Input.GetKeyDown (KeyCode.O)) 
		{
			health.MyCurrentValue += 10;
		}
		//debug EXP K = +10xp; L = -10xp.
		if (Input.GetKeyDown (KeyCode.K)) 
		{
			exp.MyCurrentValue += 50;
		}
		if (Input.GetKeyDown (KeyCode.L)) 
		{
			exp.MyCurrentValue -= 50;
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