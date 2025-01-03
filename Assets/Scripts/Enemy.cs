using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Enemy : MonoBehaviour, IGetAttacked
{
    [SerializeField] float health;
    [SerializeField] Animator animator;
    [SerializeField] Transform player;
    [SerializeField] float ThresholdDistance;
    [SerializeField] float speed,dashSpeed;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] Projectile projectile;
    [SerializeField] Transform projectileSendingPosition;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] RectTransform UIHealthTransform;
    StateManager stateManager;
    State idle;
    State runState;
    State dashState;
    State projectileSendingState;
    DeathState deathState;
    AttackState attack1, attack2;
    State rage;
    Transition FarAwayTransition;
    Transition toIdleTransition;
    Transition finishRunning;
    Transition closeTransition;
    Transition rageTransition;

    Rigidbody2D rb;
    Vector2 scale;
    bool haveRaged;
    Vector2 HealthUIScale;
    float startingHealthUIWidth;
    private void Start()
    {
        HealthUIScale = UIHealthTransform.sizeDelta;
        startingHealthUIWidth = HealthUIScale.x;
        haveRaged = false;
        idle = new IdleState(animator);
        deathState = new DeathState(animator);
        rb = GetComponent<Rigidbody2D>();
        runState = new RunState(rb, animator, speed);
        stateManager = new StateManager(idle);
        dashState = new DashState(rb, animator, dashSpeed);
        attack1 = new AttackState(transform, animator, playerLayer, "attack1", 10);
        attack2 = new AttackState(transform, animator, playerLayer, "attack2", 20);
        rage = new RageState(animator, () => 
        { 
            health += 40f*Time.deltaTime; 
            HealthUIScale.x = health * startingHealthUIWidth / 100f;
            UIHealthTransform.sizeDelta = HealthUIScale;
        },
        ()=> { attack1.ChangeAttackDmg(15);attack2.ChangeAttackDmg(25);});
        projectileSendingState = new SendProjectileState(animator, SendProjectile);
        rageTransition = new Transition(() =>
        {
            if( health <= 40f && haveRaged==false)
            {
                haveRaged = true;
                return true;
            }
            return false;
        }, new StatePourcentage(rage,100f));
        FarAwayTransition = new Transition(() =>
        {
            return (ThresholdDistance < Mathf.Abs(transform.position.x - player.position.x));
        },new StatePourcentage(projectileSendingState,33), new StatePourcentage(runState, 33), new StatePourcentage(dashState, 34f));//,new StatePourcentage(dashState,50f)

        toIdleTransition = new Transition(() =>
        {
            return stateManager.GetCurrentState().isStateFinsihed;
        }, new StatePourcentage(idle, 100));

        finishRunning = new Transition(() =>
        {
            return (ThresholdDistance >= Mathf.Abs(transform.position.x - player.position.x));
        }, new StatePourcentage(idle, 100));

        closeTransition = new Transition(() =>
        {
            return (ThresholdDistance >= Mathf.Abs(transform.position.x - player.position.x));
        }, new StatePourcentage(attack2, 50f), new StatePourcentage(attack1, 50f));

        stateManager.AddStateTransition(projectileSendingState, toIdleTransition);
        stateManager.AddStateTransition(idle, rageTransition);
        stateManager.AddStateTransition(rage, toIdleTransition);
        stateManager.AddStateTransition(idle, closeTransition);
        stateManager.AddStateTransition(idle, FarAwayTransition);
        stateManager.AddStateTransition(dashState, toIdleTransition);
        stateManager.AddStateTransition(dashState, finishRunning);
        stateManager.AddStateTransition(runState, finishRunning);
        stateManager.AddStateTransition(attack2, toIdleTransition);
        stateManager.AddStateTransition(attack1, toIdleTransition);
        scale = transform.localScale;
        GameManager.onGameStateChanges += GameManager_onGameStateChanges;
    }

    private void GameManager_onGameStateChanges(GameStates state)
    {
        if(state== GameStates.Finished)
        {
            stateManager.ChangeState(idle);
        }
    }

    public void GetAttacked(int damage)
    {
        health -= damage;
        HealthUIScale.x = health * startingHealthUIWidth / 100f;
        UIHealthTransform.sizeDelta = HealthUIScale;
        if (health<= 0)
        {
            stateManager.ChangeState(deathState);
            var deathParticle = Instantiate(deathParticles, transform.position, Quaternion.identity);
            deathParticle.Play();
            Destroy(deathParticle.gameObject, 5);
            GameManager.instance.FinishGame();
        }
    }

    private void Update()
    {
        stateManager.UpdateStates(Time.deltaTime);
        if (stateManager.GetCurrentState() is DeathState) return;
        scale.x = transform.position.x > player.position.x ? -1 : 1;
        transform.localScale = scale;
    }
    public void FinishMovement()
    {
        stateManager.GetCurrentState().FinishMovement();
    }
    public void EndCurrentState()
    {
        stateManager.GetCurrentState().FinishState();
    }
    public void StartAction()
    {
        stateManager.GetCurrentState().StartAction();
    }
    void SendProjectile()
    {
        Vector2 direction = (player.position - transform.position).normalized;
        Instantiate(projectile, projectileSendingPosition.position, Quaternion.identity).Setup(direction);

    }
}

public interface IGetAttacked
{
    public void GetAttacked(int damage);

}

