using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : Character {

    [SerializeField]

    private Transform playerPos;

    private IState currentState;

    public double MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

    [SerializeField]
    private float initHealth = 100;

    public Transform Target
    {
        get
        {
            return playerPos;
        }

        set
        {
            playerPos = value;
        }
    }

    public override bool IsDead
    {
        get
        {
            return initHealth <= 0;
        }
    }

    protected void Awake()
    {
        MyAttackRange = 0.5;
        ChangeState(new IdleState());
    }

    protected override void Update()
    {
        if (!EnemyisAttacking)
        {
            MyAttackTime += Time.deltaTime;
        }

        if (!IsDead)
        {
            if(!TakingDamage)
            {
                currentState.Update();
            }

            base.Update();
        }

     
    }


    public void ChangeState(IState newState)
    {
        if (currentState != null)
        {
            currentState.Exit();
        }

        currentState = newState;

        currentState.Enter(this);
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
    }

    public override IEnumerator TakeDamage()
    {
        initHealth -= 10;

        if (!IsDead)
        {
            MyAnimator.SetTrigger("damage");
        }
        else
        {
            MyAnimator.SetTrigger("death");

            yield return new WaitForSeconds(5);
            Destroy(gameObject);
        }
    }

}
