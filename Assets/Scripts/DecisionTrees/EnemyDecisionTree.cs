using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionTree : MonoBehaviour
{
    public EnemyAI _enemy;

    QuestionNode _isPlayerInRange;
    QuestionNode _isPlayerInSight;
    QuestionNode _isHacked;
    
    ActionNode _actionShoot;
    ActionNode _actionPatrol;
    ActionNode _actionSeek;
    ActionNode _actionHacked;

    public INode _init;

    public EnemyDecisionTree(EnemyAI enemy)
    {
        _enemy = enemy;
    }

    public void SetNodes()
    {
        _actionShoot = new ActionNode(_enemy.ActionShoot);
        _actionSeek = new ActionNode(_enemy.ActionSeek);
        _actionPatrol = new ActionNode(_enemy.ActionPatrol);
        _actionHacked = new ActionNode(_enemy.ActionHacked);

        _isPlayerInRange = new QuestionNode(_enemy.QuestionDistanceShoot, _actionShoot, _actionSeek);
        _isHacked = new QuestionNode(_enemy.QuestionHack,_actionHacked,_isPlayerInRange);
        _isPlayerInSight = new QuestionNode(_enemy.QuestionIsPlayerOnSight, _isHacked, _actionPatrol);

        _init = _isPlayerInSight;
    }

    
}
