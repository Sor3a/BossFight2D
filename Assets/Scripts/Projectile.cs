using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [SerializeField] int damage;
    [SerializeField] float speed;
    Rigidbody2D rb;
    Vector2 direction;

    public void Setup(Vector2 direction)
    {
        this.direction = direction;
        rb = GetComponent<Rigidbody2D>();
        transform.right = direction;
        Destroy(gameObject, 6);
    }
    private void Update()
    {
        rb.velocity = direction * speed;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log(collision.gameObject.name);
        if(collision.tag == "Player")
        {
            if(collision.TryGetComponent(out IGetAttacked getAttacked))
            {
                getAttacked.GetAttacked(damage);
                Destroy(gameObject);
            }
        }
    }
}
