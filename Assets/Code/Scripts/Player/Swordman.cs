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
    Healthbar healthbar;

    [Header("Dash")]
    [SerializeField] float dashingPower = 10f;
    [SerializeField] float dashingTime = 0.2f;
    [SerializeField] float dashingCooldown = 1f;
    bool canDash = true;
    bool isDashing;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        
        health = maxHealth;
        healthbar = FindObjectOfType<Healthbar>().GetComponent<Healthbar>();
        healthbar.SetMaxHealth(health);
    }

    void Update()
    {
        if (isDashing)
            return;

        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftShift) && canDash)
        {
            StartCoroutine(Dash());
            StopCoroutine(Dash());
        }
    }

    void FixedUpdate()
    {
        if (isDashing)
            return;

        Move();
    }

    //MOVEMENT
    void Move()
    {
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        if (movement.x < 0 && facingRight)
        {
            Flip();
        }
        else if (movement.x > 0 && !facingRight)
        {
            Flip();
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    //HEALTH AND DIE
    public void TakeDamage(int damage)
    {
        health -= damage;
        healthbar.SetHealth(health);
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

    //DASH
    IEnumerator Dash()
    {
        canDash = false;
        isDashing = true;
        float originalGravity = rb.gravityScale;
        rb.gravityScale = 0f;

        if (movement.y > 0)
            rb.velocity = new Vector2(0f, transform.localScale.y * dashingPower);
        else if (movement.y < 0)
            rb.velocity = new Vector2(0f, -transform.localScale.y * dashingPower);
        else if (facingRight)
            rb.velocity = new Vector2(transform.localScale.x * dashingPower, 0f);
        else if (!facingRight)
            rb.velocity = new Vector2(-transform.localScale.x * dashingPower, 0f);

        yield return new WaitForSeconds(dashingTime);

        rb.gravityScale = originalGravity;
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);

        canDash = true;
    }
}
