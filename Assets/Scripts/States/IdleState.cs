using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    Animator animator;


    public IdleState(Animator animator)
    {
        this.animator = animator;

    }
    public override void StartState()
    {
        animator.SetBool("idle", true);
        isStateFinsihed = false;
    }
    public override void EndState()
    {
        animator.SetBool("idle", false);
    }
}
