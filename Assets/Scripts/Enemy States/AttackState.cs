using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : IState
{
    private Enemy parent;

    private float attackCD = 3;

    private float extraRange = 0.1f;

    public void Enter(Enemy parent)
    {
        this.parent = parent;

    }

    public void Exit()
    {

    }

    public void Update()
    {
        if (parent.MyAttackTime >= attackCD && !parent.isAttacking)
        {
            parent.MyAttackTime = 0;
            parent.StartCoroutine(Attack());
        }


        if (parent.Target != null)
        {
            float distance = Vector2.Distance(parent.Target.position, parent.transform.position);

            if (distance >= parent.MyAttackRange+extraRange && !parent.isAttacking)
            {
                parent.ChangeState(new FollowState());
            }
    
        }
        else
        {
            parent.ChangeState(new FollowState());
        }
    }

    public IEnumerator Attack()
    {
        parent.isAttacking = true;
        parent.MyAnimator.SetTrigger("attack");
        yield return new WaitForSeconds(parent.MyAnimator.GetCurrentAnimatorStateInfo(1).length);
        parent.isAttacking = false;
    }
}
