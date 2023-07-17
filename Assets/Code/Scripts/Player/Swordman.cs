using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Vector2 movement;
    bool facingRight = true;

    [Header("Health")]
    [SerializeField] int maxHealth = 10;
    [SerializeField] GameObject gameOverUI;
    int health;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;
    }

    void Update()
    {
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
    }

    void FixedUpdate()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if(movement.x < 0 && facingRight)
        {
            Flip();
        }
        else if(movement.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if(health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        gameOverUI.SetActive(true);
        Destroy(gameObject);
    }
}
