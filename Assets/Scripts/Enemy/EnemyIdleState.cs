using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyIdleState : EnemyState
{
    float idleDuration = 5;
    float currentIdleDuration = 0;
    public EnemyIdleState(StateMachine sm, EnemyAI enemy) : base(sm, enemy)
    {
    }

    public override void Awake()
    {
        base.Awake();
        idleDuration = Random.Range(0, 5);
        currentIdleDuration = 0;
        _enemy.speed = 0;
    }

    public override void Execute()
    {
        base.Execute();
        currentIdleDuration += Time.deltaTime;

        if (currentIdleDuration > idleDuration)
            _sm.SetState<EnemyPatrolState>();

        if (_enemy.playerInSight)
            _sm.SetState<EnemyShootState>();
    }
}
