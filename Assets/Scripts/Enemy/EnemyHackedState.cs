using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHackedState : EnemyState
{
    float hackDuration = 3;
    float currentHackDuration = 0;

    public EnemyHackedState(StateMachine sm, EnemyAI enemy) : base(sm, enemy)
    {
    }

    public override void Awake()
    {
        base.Awake();
        currentHackDuration = 0;
        _enemy.animator.SetBool("Hack", true);
        _enemy.audioSource.PlayOneShot(_enemy.hackSound);
        ParticleSystem obj = GameObject.Instantiate(_enemy.hackParticle, _enemy.hackParticleSpawn.transform.position, _enemy.transform.rotation);
        _enemy.hackParticle.Play();
        GameObject.Destroy(obj, 3f);

    }

    public override void Execute()
    {
        base.Execute();

        currentHackDuration += Time.deltaTime;

        if (currentHackDuration > hackDuration)
        {
            Sleep();
        }
    }

    public override void Sleep()
    {
        base.Sleep();
        _enemy.animator.SetBool("Hack", false);
        _enemy.hack = false;
    }
}
