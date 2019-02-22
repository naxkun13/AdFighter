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
            CalculateShootAngles();
 
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

    private void CalculateShootAngles()
    {
        if (Direction == Vector2.up) {
            rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 90);
        } else if (Direction == Vector2.down) {
            rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -90);
        } else if (Direction == Vector2.right) {
            rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 0);
        } else if (Direction == Vector2.left) {
            rot = Quaternion.Euler(transform.rotation.x, transform.rotation.y, 180);
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
            shoot();
        }

        //melee
        if (Input.GetKeyDown(KeyCode.C))
        {
            StartCoroutine(Attack());
        }


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

        //debug taking damage Y = -10HP
        if (Input.GetKey(KeyCode.Y))
        {
            StartCoroutine(TakeDamage());
        }

    }
    public void shoot()
    {
        Instantiate(bulletPrefab, firePoints[fireIndex].position, rot);
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

  
}