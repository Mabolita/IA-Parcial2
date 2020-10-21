using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActionNode : Inode
{
    public delegate void myDelegate();
    myDelegate _action;

    public ActionNode(myDelegate action)
    {
        _action = action;
    }
    public void Execute()
    {
        _action();
    }
}
