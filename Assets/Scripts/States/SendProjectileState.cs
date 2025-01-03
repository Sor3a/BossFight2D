using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SendProjectileState : State
{

    Animator animator;
    System.Action sendProjectileFunc;
    public SendProjectileState(Animator animator,System.Action sendProjectileFunc)
    {
        this.animator = animator;
        this.sendProjectileFunc = sendProjectileFunc;
    }
    public override void StartState()
    {
        isStateFinsihed = false;
        animator.SetBool("projectil", true);
    }
    public override void StartAction()
    {
        sendProjectileFunc();
    }
    public override void EndState()
    {
        animator.SetBool("projectil", false);

    }


}
