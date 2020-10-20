using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyShootState : EnemyState
{
    public EnemyShootState(StateMachine sm, EnemyAI enemy) : base(sm, enemy)
    {
    }

    public override void Awake()
    {
        base.Awake();
        _enemy.speed = 0;
    }

    public override void Execute()
    {
        base.Execute();
        Vector3 dirToPlayer = (_enemy.player.transform.position - _enemy.transform.position).normalized;

        _enemy.transform.LookAt(_enemy.player.transform.position);

        if (!_enemy.playerInSight)
        {
            _sm.SetState<EnemyPatrolState>();
        }
    }
    
    void Shoot()
    {
        GameObject bullet = Object.Instantiate(_enemy.bulletPrefab);
        bullet.transform.position = _enemy.bulletSpawn.position;
        bullet.transform.up = _enemy.bulletSpawn.forward;
    }

    public override void Sleep()
    {
        base.Sleep();
        _enemy.playerInSight = false;
    }
}
