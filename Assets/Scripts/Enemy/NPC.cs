using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Character
{
    public override bool IsDead
    {
        get
        {
            throw new System.NotImplementedException();
        }
    }

    public override IEnumerator TakeDamage()
    {
        throw new System.NotImplementedException();
    }
}
