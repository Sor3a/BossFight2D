using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RunState : State
{
    Rigidbody2D rb; 
    Animator animator;
    float speed;
    float scaleX;
    Vector2 velocity;
    Transform enemy;
    public RunState(Rigidbody2D rb,Animator animator, float speed)
    {
        this.animator = animator;
        this.speed = speed;
        this.rb = rb;
        enemy = rb.transform;
        scaleX = enemy.localScale.x;
        velocity = new Vector2();
    }
    public override void StartState()
    {
        isStateFinsihed = false;
        animator.SetBool("run",true);
    }
    public override void UpdateState(float DeltaTime)
    {
        scaleX = enemy.localScale.x;
        velocity.x = scaleX* speed;
        rb.velocity = velocity;
    }
    public override void EndState()
    {
        animator.SetBool("run", false);

    }


}
