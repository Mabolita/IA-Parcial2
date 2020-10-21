using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreePrueba : MonoBehaviour
{
    //public Script de funciones _ec;

    QuestionNode _questionSight;
    QuestionNode _questionRange;
    QuestionNode _questionLost;
    
    ActionNode _actionShoot;

    public Inode _init;

    private void Start()
    {
        //ejemplos de accion y pregunta
        //_actionShoot = new ActionNode(_ec.ActionShoot);
        //_questionSight = new QuestionNode(_ec.QuestionSight, _questionRange, _questionLost);

        _init = _questionSight;
        _init.Execute();
    }

    
}
