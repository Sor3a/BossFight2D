using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class State 
{
    public bool isStateFinsihed;
    public abstract void StartState();
    public virtual void UpdateState(float deltaTime)
    {}
    public abstract void EndState();
    public virtual void FinishMovement()
    { }
    public virtual void FinishState()
    {
        isStateFinsihed = true;
    }

    public virtual void StartAction() { }
}


