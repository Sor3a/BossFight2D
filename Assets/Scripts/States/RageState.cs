using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RageState : State
{
    Animator animator;
    System.Action addHealth;
    System.Action changingAttackDmg;
    public RageState(Animator animator, System.Action addHealth, System.Action changeAttack)
    {
        this.animator = animator;
        this.addHealth = addHealth;
        changingAttackDmg = changeAttack;
    }
    public override void StartState()
    {
        animator.SetBool("rage", true);
        isStateFinsihed = false;
        changingAttackDmg(); //add the enemy dmg after raging
    }
    public override void UpdateState(float deltaTime)
    {
        addHealth();
    }
    public override void EndState()
    {
        animator.SetBool("rage", false);
    }
}
