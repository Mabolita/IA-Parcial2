using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionNode : Inode
{
    public delegate bool myDelegate();
    myDelegate _question;
    Inode _trueNode;
    Inode _falseNode;

    public QuestionNode(myDelegate question, Inode tN, Inode fN)
    {
        _question = question;
        _trueNode = tN;
        _falseNode = fN;
    }

    public void Execute()
    {
        if (_question())
        {
            _trueNode.Execute();
        }
        else
        {
            _falseNode.Execute();   
        }
    }
}
