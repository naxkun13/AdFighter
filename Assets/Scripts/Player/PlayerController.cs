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

    [SerializeField]
    private GameObject bulletPrefab;

    [SerializeField]
    private float initHealth = 50;

    public override bool IsDead {
        get { return health.MyCurrentValue <= 0; }
    }

    protected override void Start() {
        firePoints[0].transform.localScale += Vector3.left;
        base.Start();
        if (PlayerPrefs.HasKey("PlayerExp"))
            exp.Initialize(PlayerPrefs.GetFloat("PlayerExp"), 100 * MyLevel * Mathf.Pow(MyLevel, 0.4f));
        else
            exp.Initialize(1, 100 * Mathf.Pow(1, 0.4f) );

        if (PlayerPrefs.HasKey("MyHP"))
            health.Initialize(PlayerPrefs.GetFloat("MyHP"), MyLevel * 50);
        else
            health.Initialize(initHealth, initHealth);
    }

    protected override void Update() {
        if (!TakingDamage && !IsDead)
        {
            GetInput();
            GetLevel();
            LevelUp();
            levelText.text = MyLevel.ToString();
            DebugInput();
        }
        else
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;

        base.Update();
    }

    private void GetLevel() {
        exp.Initialize(exp.MyCurrentValue, 100 * MyLevel * Mathf.Pow(MyLevel, 0.4f));
    }

    private void LevelUp() {
        if (exp.MyCurrentValue == exp.MyMaxValue)
        {
            MyLevel += 1;
            exp.MyCurrentValue = 0;
            health.MyCurrentValue = health.MyMaxValue;

            PlayerPrefs.SetInt("MyLevel", MyLevel);
            PlayerPrefs.SetFloat("PlayerExp", exp.MyCurrentValue);
            PlayerPrefs.SetFloat("MyHP", health.MyCurrentValue);
        }
    }

    private void GetInput() {
        Direction = Vector2.zero;

        //moving
        if (Input.GetKey(KeyCode.W)) {
            Direction += Vector2.up;
        } else if (Input.GetKey(KeyCode.S))
        {
            Direction += Vector2.down;
        }
        
        if(Input.GetKey(KeyCode.A)) {
            firePoints[0].position.Set(0f, -180f, 0f);
            Direction += Vector2.left;
        } else if (Input.GetKey(KeyCode.D))
        {
            Direction += Vector2.right;
        }
    }

    private void DebugInput() {
        //debug HP I = -10hp; O = +10hp.
        if (Input.GetKeyDown(KeyCode.I))
            health.MyCurrentValue -= 10;
        else if (Input.GetKeyDown(KeyCode.O))
            health.MyCurrentValue += 10;
        //debug EXP K = +10xp; L = -10xp.
        else if(Input.GetKeyDown(KeyCode.K))
            exp.MyCurrentValue += 50;
        else if(Input.GetKeyDown(KeyCode.L))
            exp.MyCurrentValue -= 50;
        //shooting
        else if(Input.GetKeyDown(KeyCode.F))
            ShootBullet(0);
        //melee
        else if(Input.GetKeyDown(KeyCode.C))
            StartCoroutine(Attack());
        //debug taking damage Y = -10HP
        else if(Input.GetKey(KeyCode.Y))
            StartCoroutine(TakeDamage());
    }

    public IEnumerator Attack() {
        isAttacking = true;
        MyAnimator.SetTrigger("attack");
        yield return new WaitForSeconds(MyAnimator.GetCurrentAnimatorStateInfo(1).length);
        isAttacking = false;
    }

    public override IEnumerator TakeDamage() {
        health.MyCurrentValue -= 10;

        if (!IsDead) {
            MyAnimator.SetTrigger("damage");
            PlayerPrefs.SetFloat("MyHP", health.MyCurrentValue);
        }
        else
            MyAnimator.SetTrigger("death");

        yield return null;
    }

    public void ShootBullet(int value) {
        if (Direction == Vector2.right || Direction == Vector2.zero) {
            GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0,0,0)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.right);
        } else {
            GameObject tmp = (GameObject)Instantiate(bulletPrefab, transform.position, Quaternion.Euler(new Vector3(0, 0, 180)));
            tmp.GetComponent<Bullet>().Initialize(Vector2.left);
        }
	}

    public void AddExp(int expToAdd) {
        exp.MyCurrentValue += expToAdd;
        PlayerPrefs.SetFloat("PlayerExp", exp.MyCurrentValue);
    }
}