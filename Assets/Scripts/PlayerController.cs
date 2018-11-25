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

	[SerializeField]
	private Transform[] firePoints;

	private int fireIndex = 1;

	[SerializeField]
	private GameObject bulletPrefab;

	private float initHealth = 100;


	protected override void Start()
	{
		firePoints [0].transform.localScale += Vector3.left;
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


		if (Input.GetKeyDown (KeyCode.F)) 
		{
			shoot ();
		}	


		if (Input.GetKey (KeyCode.W)) 
		{
			direction += Vector2.up;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			fireIndex = 1;
			direction += Vector2.right;
		}
		if (Input.GetKey (KeyCode.A)) 
		{
			fireIndex = 0;
			firePoints [0].position.Set (0f, -180f, 0f);
			direction += Vector2.left;
		}
		if (Input.GetKey (KeyCode.S)) 
		{
			direction += Vector2.down;
		}
	}
	public void shoot ()
	{
		Instantiate (bulletPrefab, firePoints[fireIndex].position , Quaternion.identity);

	}
}