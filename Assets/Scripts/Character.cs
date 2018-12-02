using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
	[SerializeField]
	private float speed;

	[SerializeField]
	public int level;


	private Rigidbody2D rb2d;

	public bool IsMoving {
		get { 
			return direction.x != 0 || direction.y != 0;
		}
	}

	private Animator animator;
	protected Vector2 direction;

	public int MyLevel
	{
		get
		{
			return level;
		}
		set
		{ 
			level = value;
		}
	}



	protected virtual void Start () {
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();

	}



	protected virtual void Update () {
		HandleLayers ();

	}

	private void FixedUpdate()
	{
		Move();

	}


	public void Move()
	{
		rb2d.velocity = direction.normalized * speed;
	}

	public void AnimateMovement (Vector2 direction)
	{
		// переключение условий в слоях аниматора
		//  animator.SetLayerWeight (1,1); 
		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);
	}

	public void ActivateLayer(string layername)
	{
		for (int i = 0; i < animator.layerCount; i++) {
			animator.SetLayerWeight (i, 0);
		}
	}

	public void HandleLayers()
	{
		if (IsMoving) {
			AnimateMovement  (direction);
			ActivateLayer("WalkLayer");

		} else 
		{
			ActivateLayer ("BaseLayer");
		}
	}


}
