using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    bool isAttacking;
    float range;
    [SerializeField] int damage;
    [SerializeField] LayerMask enemyLayer;
    [SerializeField] ParticleSystem attackEffect;
    [SerializeField] AudioSource attackAudioSource;
    private void Start()
    {
        isAttacking = false;
        range = 0f;
    }
    public void StartAttacking(float attackRange)
    {
        isAttacking = true;
        range = attackRange;
    }
    public void EndAttacking()
    {
        isAttacking = false;
        range = 0f;
    }

    private void Update()
    {
        if (isAttacking)
        {
            RaycastHit2D rayCastHit = Physics2D.Raycast(transform.position, Vector2.right * transform.localScale.x, range, enemyLayer);
            if (rayCastHit)
            {
                if(rayCastHit.transform.TryGetComponent(out IGetAttacked getAttacked))
                {
                    attackAudioSource.Play();
                    getAttacked.GetAttacked(damage);
                    attackEffect.transform.position = rayCastHit.point;
                    attackEffect.Play();
                    isAttacking = false;
                }
            }    
        }
    }
}
//    
