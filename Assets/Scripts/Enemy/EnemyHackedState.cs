using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHackedState : EnemyState
{
    public EnemyHackedState(StateMachine sm, EnemyAI enemy) : base(sm, enemy)
    {
    }

    public override void Awake()
    {
        base.Awake();
        _enemy.animator.SetFloat("Speed", 0);
        _enemy.animator.SetFloat("AngularSpeed", 0);
    }

    public override void Execute()
    {
        base.Execute();
        Debug.Log("Hack");
    }

    public override void Sleep()
    {
        base.Sleep();
        _enemy.hack = false;
    }
}
