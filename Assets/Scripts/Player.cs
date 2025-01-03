using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Player : MonoBehaviour,IGetAttacked
{
    [SerializeField] Animator animator;
    [SerializeField] Rigidbody2D rb;
    [SerializeField] float jumpForce;
    [SerializeField] Transform playerFeet;
    [SerializeField] LayerMask groundLayer;
    [SerializeField] float speed;
    [SerializeField] float health;
    [SerializeField] ParticleSystem deathParticles;
    
    public bool isDead;
    Vector2 localScale;
    bool isGrounded;
    float x;
    [SerializeField] RectTransform UIHealthTransform;
    Vector2 HealthUIScale;
    float startingHealthUIWidth;

    private void Start()
    {
        localScale = new Vector2(1, 1);
        isDead = false;
        HealthUIScale = UIHealthTransform.sizeDelta;
        startingHealthUIWidth = HealthUIScale.x;

    }
    private void Update() 
    {
        if (isDead) return;
        if (GameManager.instance.GameState == GameStates.Finished)
        {
            animator.SetBool("isAttacking", false);
            animator.SetBool("isRunning", false);
            animator.SetBool("isJumping", false);
            return;
        }
        x = Input.GetAxis("Horizontal");
        if (x!=0)
        {
            animator.SetBool("isRunning", true);
            
            

            if (x > 0) localScale.x = 1;
            else localScale.x = -1;
        }
        else
            animator.SetBool("isRunning", false);
        transform.localScale = localScale;
        Attack();
        Jump();

    }
    
    private void FixedUpdate() 
    {
        if(x!=0)
        {
            Vector2 velocity = rb.velocity;
            string currentClip = (animator.GetCurrentAnimatorClipInfo(0))[0].clip.name;
            if (currentClip != "attack")
            {
                velocity.x = x * Time.deltaTime * speed;
            }
            else velocity.x = 0;
            rb.velocity = velocity;
        }
    }

    void Jump()
    {

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded && animator.GetBool("isAttacking") == false)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            animator.SetBool("isJumping", true);
        }
        if (animator.GetBool("isJumping") == true && rb.velocity.y < 0 && isGrounded)
        {
            animator.SetBool("isJumping", false);
        }

        if (!isGrounded)
            animator.SetFloat("JumpingForce", rb.velocity.y);


        isGrounded = Physics2D.Raycast(playerFeet.position, Vector2.down, 0.2f, groundLayer);
    }


    void Attack()
    {

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            animator.SetInteger("attackType", 0);
            animator.SetBool("isAttacking", true);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1))
        {
            animator.SetInteger("attackType", 1);
            animator.SetBool("isAttacking", true);
        }
        if (Input.GetKeyUp(KeyCode.Mouse0) || Input.GetKeyUp(KeyCode.Mouse1))
        {
            animator.SetBool("isAttacking", false);
        }
    }

    public void GetAttacked(int damage)
    {
        if (isDead) return;
        health -= damage;
        HealthUIScale.x = health * startingHealthUIWidth / 100f;
        UIHealthTransform.sizeDelta = HealthUIScale;
        if (health<=0)
        {
            isDead = true;
            animator.SetBool("death", true);
            var deathParticle = Instantiate(deathParticles, transform.position, Quaternion.identity);
            deathParticle.Play();
            Destroy(deathParticle.gameObject, 5);
            GameManager.instance.FinishGame();

        }
    }


}


