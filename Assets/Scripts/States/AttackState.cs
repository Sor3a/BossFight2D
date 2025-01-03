using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    string attackStateName; 
    bool isAttacking;
    Animator animator;
    Transform transform;
    float range;
    int damage;
    LayerMask playerLayer;

    public AttackState(Transform t,Animator animator,LayerMask playerLayer,string attackStateName, int damage, float range=2.1f)
    {
        this.animator = animator;
        this.attackStateName = attackStateName;
        this.range = range;
        transform = t;
        this.damage = damage;
        this.playerLayer = playerLayer;
    }
    public void ChangeAttackDmg(int newAttackDmg)
    {
        damage = newAttackDmg;
    }
    public override void StartState()
    {
        isStateFinsihed = false;
        animator.SetBool(attackStateName, true);
        isAttacking = false;
    }

    public override void EndState()
    {
        animator.SetBool(attackStateName, false);

    }
    public override void UpdateState(float deltaTime)
    {
        Attack();
    }
    public override void StartAction()
    {
        isAttacking = true;
    }
    public override void FinishMovement()
    {
        isAttacking = false;
    }
    void Attack()
    {
        if (!isAttacking) return;
        RaycastHit2D rayCastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, range, playerLayer);
        if (rayCastHit)
        {
            if (rayCastHit.transform.TryGetComponent(out IGetAttacked getAttacked))
            {
                getAttacked.GetAttacked(damage);
                isAttacking = false;
            }
        }
    }

}
