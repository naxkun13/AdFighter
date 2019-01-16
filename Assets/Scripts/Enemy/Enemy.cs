using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Enemy : NPC {

	[SerializeField]
	private CanvasGroup healthGroup;

	private Transform playerPos;

    private IState currentState;

    public float MyAttackRange { get; set; }

    public float MyAttackTime { get; set; }

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

    protected void Awake()
    {
        MyAttackRange = 1;
        ChangeState(new IdleState());
    }

	protected override void Update ()
	{
        if (!isAttacking)
        {
            MyAttackTime += Time.deltaTime;
        }

        currentState.Update();

		base.Update ();
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

		
}
