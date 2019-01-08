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
			return Direction.x != 0 || Direction.y != 0;
		}
	}

	private Animator animator;
    private Vector2 direction;

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

    public Vector2 Direction
    {
        get
        {
            return direction;
        }

        set
        {
            direction = value;
        }
    }

    public float Speed
    {
        get
        {
            return speed;
        }

        set
        {
            speed = value;
        }
    }

    protected virtual void Start () {
		animator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();

	}



	protected virtual void Update () {
		AnimateMovement  (Direction);

	}

	private void FixedUpdate()
	{
		Move();

	}


	public void Move()
	{
		rb2d.velocity = Direction.normalized * Speed;
	}

	public void AnimateMovement (Vector2 direction)
	{
		// переключение условий в слоях аниматора
		//  animator.SetLayerWeight (1,1); 
		animator.SetFloat ("x", direction.x);
		animator.SetFloat ("y", direction.y);
	}

	/*public void ActivateLayer(string layername)
	{
		for (int i = 0; i < animator.layerCount; i++) {
			animator.SetLayerWeight (i, 0);
		}
	}

	public void HandleLayers()
	{
		if (IsMoving) {
			ActivateLayer("WalkLayer");
			AnimateMovement  (direction);


		} else 
		{
			ActivateLayer ("BaseLayer");
		}
	}
*/

}
