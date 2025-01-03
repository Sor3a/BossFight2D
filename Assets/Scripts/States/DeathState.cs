using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathState : State
{
    Animator animator;
    public DeathState(Animator animator)
    {
        this.animator = animator;
    }
    public override void StartState()
    {
        isStateFinsihed = false;
        animator.SetBool("death", true);
    }
    public override void EndState()
    {
    }


}
