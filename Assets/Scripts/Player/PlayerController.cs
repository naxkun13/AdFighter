using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : Character {


    [SerializeField]
    private Stat exp;

    [SerializeField]
    private Text levelText;

    [SerializeField]
    private Transform[] firePoints;

    private int fireIndex = 1;

    [SerializeField]
    private GameObject bulletPrefab;

    private Quaternion rot;

    [SerializeField]
    private float initHealth = 50;


    public override bool IsDead
    {
        get
        {
            return health.MyCurrentValue <= 0;
        }
    }

    protected override void Start()
    {
        firePoints[0].transform.localScale += Vector3.left;
        health.Initialize(initHealth, initHealth);
        base.Start();
        rot = new Quaternion(0, 0, 0, 0);
    }

    protected override void Update()
    {
        if (!TakingDamage && !IsDead)
        {
            GetInput();
            GetLevel();
            LevelUp();
            levelText.text = MyLevel.ToString();
            //CalculateShootAngles();
            DebugInput();
 
        }
        else
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
        base.Update();
    }


    private void GetLevel()
    {
        exp.Initialize(exp.MyCurrentValue, 100 * MyLevel * Mathf.Pow(MyLevel, 0.4f));
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
        Direction = Vector2.zero;

        //moving
        if (Input.GetKey(KeyCode.W))
        {
            fireIndex = 2;
            Direction += Vector2.up;
        }
        if (Input.GetKey(KeyCode.D))
        {
            fireIndex = 1;
            Direction += Vector2.right;
        }
        if (Input.GetKey(KeyCode.A))
        {
            fireIndex = 0;
            firePoints[0].position.Set(0f, -180f, 0f);
            Direction += Vector2.left;
        }
        if (Input.GetKey(KeyCode.S))
        {
            fireIndex = 3;
            Direction += Vector2.down;
        }

      

    }

    private void DebugInput()
    {
        //debug HP I = -10hp; O = +10hp.
        if (Input.GetKeyDown(KeyCode.I))
        {
            health.MyCurrentValue -= 10;
        }
        if (Input.GetKeyDown(KeyCode.O))
        {
            health.MyCurrentValue += 10;
        }
        //debug EXP K = +10xp; L = -10xp.
        if (Input.GetKeyDown(KeyCode.K))
        {
            exp.MyCurrentValue += 50;
        }
        if (Input.GetKeyDown(KeyCode.L))
        {
            exp.MyCurrentValue -= 50;
        }

        //shooting
        if (Input.GetKeyDown(KeyCode.F))
        {
            ShootBullet(0);
        }

        //melee
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Attack());
        }

        //debug taking damage Y = -10HP
        if (Input.GetKey(KeyCode.Y))
        {
            StartCoroutine(TakeDamage());
        }
    }



    public IEnumerator Attack()
    {
        isAttacking = true;
        MyAnimator.SetTrigger("attack");
        yield return new WaitForSeconds(MyAnimator.GetCurrentAnimatorStateInfo(1).length);
        isAttacking = false;
    }

    public override IEnumerator TakeDamage()
    {
        health.MyCurrentValue -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("death");
        }
        yield return null;
    }

    public void ShootBullet(int value)
    {
        if (Direction == Vector2.right || Direction == Vector2.zero)
        {
           GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        }
        else
        {
            GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
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
		Direction = Vector2.zero;

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
			fireIndex = 2;
			Direction += Vector2.up;
		}
		if (Input.GetKey (KeyCode.D)) 
		{
			fireIndex = 1;
			Direction += Vector2.right;
            transform.rotation = Quaternion.Euler(0, 0, 0);
        }
		if (Input.GetKey (KeyCode.A)) 
		{
			fireIndex = 0;
			firePoints [0].position.Set (0f, -180f, 0f);
			Direction += Vector2.left;
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }
		if (Input.GetKey (KeyCode.S)) 
		{
			fireIndex = 3;
			Direction += Vector2.down;
        }
	}
	public void shoot ()
	{
		Instantiate (bulletPrefab, firePoints[fireIndex].position , rot);
	}
}