using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

class FollowState : IState
{
    private Enemy parent;

    public void Enter(Enemy parent)
    {
        this.parent = parent;
    }

    public void Exit()
    {
        /*пока что триггера на выход из этого стейта нет, 
        но я сделал на случай, если захотим добавить какие-нибудь скиллы, 
        заставляющие мобов переставать атаковать игрока*/
        parent.Direction = Vector2.zero;
    }

    public void Update()
    {
        //ищет местоположение игрока
        parent.Direction = (parent.Target.transform.position - parent.transform.position).normalized;

        //выдвигается в его сторону
        parent.transform.position = Vector2.MoveTowards(parent.transform.position, parent.Target.position, parent.Speed * Time.deltaTime);

        float distance = Vector2.Distance(parent.Target.position, parent.transform.position);

        if (distance <= parent.MyAttackRange)
        {
            parent.ChangeState(new AttackState());
        }
    }
}