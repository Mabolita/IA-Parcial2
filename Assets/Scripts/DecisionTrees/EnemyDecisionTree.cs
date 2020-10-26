using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDecisionTree : MonoBehaviour
{
    public EnemyAI enemy;

    QuestionNode _isPlayerInRange;
    QuestionNode _isPlayerInSight;
    
    ActionNode _actionShoot;
    ActionNode _actionPatrol;
    ActionNode _actionSeek;

    public INode _init;

    private void Start()
    {
        _actionShoot = new ActionNode(enemy.ActionShoot);
        _actionSeek = new ActionNode(enemy.ActionSeek);
        _actionPatrol = new ActionNode(enemy.ActionPatrol);

        _isPlayerInSight = new QuestionNode(enemy.QuestionIsPlayerOnSight, _isPlayerInRange, _actionPatrol);
        _isPlayerInRange = new QuestionNode(enemy.QuestionDistanceShoot, _actionShoot, _actionSeek);
        _init.Execute();
    }

    
}
