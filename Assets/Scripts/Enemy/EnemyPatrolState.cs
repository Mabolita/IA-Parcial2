using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrolState : EnemyState
{
    public EnemyPatrolState(StateMachine sm, EnemyAI enemy) : base(sm, enemy)
    {

    }

    public override void Awake()
    {
        base.Awake();
        _enemy.speed = 5;
    }

    public override void Execute()
    {
        base.Execute();

        Transform target = _enemy.waypoints[_enemy.currentWaypointTarget];

        _enemy.transform.forward = Vector3.Slerp(_enemy.transform.forward, target.position - _enemy.transform.position, 0.15f);
        _enemy.transform.position += _enemy.transform.forward * _enemy.speed * Time.deltaTime;

        if (Vector3.Distance(_enemy.transform.position, target.position) < .5)
        {
            if (_enemy.currentWaypointTarget < _enemy.waypoints.Count - 1)
            {
                _enemy.currentWaypointTarget++;
            }
            else
            {
                _enemy.waypoints.Reverse();
                _enemy.currentWaypointTarget = 0;
            }
        }

        if (_enemy.playerInRange)
        {
            _sm.SetState<EnemySeekState>();
        }
    }
}
