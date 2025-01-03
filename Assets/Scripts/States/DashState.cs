using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashState : State
{
    Rigidbody2D rb;
    Animator animator;
    float speed;
    float scaleX;
    Vector2 velocity;
    Transform enemy;
    bool stopDashing = false;
    public DashState(Rigidbody2D rb, Animator animator, float DashSpeed)
    {
        this.animator = animator;
        this.speed = DashSpeed;
        this.rb = rb;
        enemy = rb.transform;
        scaleX = enemy.localScale.x;

    }
    public override void StartState()
    {
        isStateFinsihed = false;
        animator.SetBool("dash", true);
        stopDashing = false;
    }

    public override void FinishMovement()
    {
        stopDashing = true;
    }
    public override void UpdateState(float deltaTime)
    {
        if (stopDashing) return;

        scaleX = enemy.localScale.x;
        velocity.x = scaleX * speed;
        rb.velocity = velocity;
    }
    public override void EndState()
    {
        animator.SetBool("dash", false);

    }
}
