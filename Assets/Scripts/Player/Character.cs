using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : MonoBehaviour {
    [SerializeField]
    private float speed;

    [SerializeField]
	public int level;

    [SerializeField]
    protected Stat health;

    [SerializeField]
    private Collider2D swordColliderR;
    [SerializeField]
    private Collider2D swordColliderL;

    public abstract bool IsDead { get; }

    [SerializeField]
    private List<string> DamageSources;

    private Rigidbody2D rb2d;
    public Vector2 direction;

    public bool TakingDamage { get; set; }

    public bool IsMoving {
		get { 
			return direction.x != 0 || direction.y != 0;
		}
	}

    public Animator MyAnimator { get; set; }



    public bool EnemyisAttacking { get; set; }

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

    public Collider2D SwordColliderR
    {
        get
        {
            return swordColliderR;
        }
    }
    public Collider2D SwordColliderL
    {
        get
        {
            return swordColliderL;
        }
    }

    protected virtual void Start () {
		MyAnimator = GetComponent<Animator> ();
		rb2d = GetComponent<Rigidbody2D>();

	}



	protected virtual void Update () {
        if (!TakingDamage && !IsDead)
        {
            HandleLayers();
        }
    }

    public abstract IEnumerator TakeDamage();

    private void FixedUpdate()
    {
        if (!TakingDamage && !IsDead)
        {
            Move();
            ResetValues();
        }
    }


	public void Move()
	{
		rb2d.velocity = direction.normalized * Speed;
	}


    public void ActivateLayer(string layerName)
	{
        //сбрасывание значения веса слоя после выполнения
		for (int i = 0; i < MyAnimator.layerCount; i++) {
			MyAnimator.SetLayerWeight (i, 0);
		}
        //установка нужного слоя в значение веса - 1
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
      
        else if (EnemyisAttacking)
        {
            ActivateLayer("Enemy_Attack");
        }
        else
        {
            ActivateLayer("Idle");
        }
        if (isAttacking)
        {
            ActivateLayer("Attack");
            MyAnimator.SetTrigger("attack");
        }
    }

    public void MeleeAttack()
    {
        if (gameObject.transform.position.x > 0.1)
        {
            swordColliderR.enabled = true;
        } 
        else if (gameObject.transform.position.x < -0.1)
        {
            swordColliderL.enabled = true;
        }
    }


    public virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (DamageSources.Contains(other.tag))
        {
            StartCoroutine(TakeDamage());
        }
    }

    private void ResetValues()
    {
        isAttacking = false;
    }
}
