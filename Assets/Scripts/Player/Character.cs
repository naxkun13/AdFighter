using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    [SerializeField]
    private float speed;

    [SerializeField]
	public int level;


	private Rigidbody2D rb2d;
    public Vector2 direction;

    public bool IsMoving {
		get { 
			return direction.x != 0 || direction.y != 0;
		}
	}

    public Animator MyAnimator { get; set; }

    

    public bool isAttacking { get; set; }

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
		MyAnimator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();

	}



	protected virtual void Update () {
	//	AnimateMovement  (Direction);
        HandleLayers();
	}

	private void FixedUpdate()
	{
		Move();

	}


	public void Move()
	{
		rb2d.velocity = direction.normalized * Speed;
	}


    
	public void ActivateLayer(string layerName)
	{
		for (int i = 0; i < MyAnimator.layerCount; i++) {
			MyAnimator.SetLayerWeight (i, 0);
		}
        MyAnimator.SetLayerWeight(MyAnimator.GetLayerIndex(layerName), 1);
	}

	public void HandleLayers()
	{
		if (IsMoving)
        {
			ActivateLayer("Walk");

            MyAnimator.SetFloat("x", direction.x);
            MyAnimator.SetFloat("y", direction.y);
        }
        else if (isAttacking)
		{
			ActivateLayer ("Attack");
		}
        else
        {
            ActivateLayer("Idle");
        }
	}


}
