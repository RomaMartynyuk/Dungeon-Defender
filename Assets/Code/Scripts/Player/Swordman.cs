using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Swordman : MonoBehaviour
{
    Animator animator;
    public bool canMove = true;
    public bool isDied = false;

    [Header("Movement")]
    [SerializeField] float moveSpeed = 5f;
    Rigidbody2D rb;
    Collider2D col;
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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();
        healthbar = FindObjectOfType<Healthbar>().GetComponent<Healthbar>();
    }

    void Start()
    {
        health = maxHealth;
        healthbar.SetMaxHealth(health);
    }

    void Update()
    {
        if (!canMove)
            return; 

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
        if (!canMove)
            return;

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

        if (movement.x < 0 || movement.x > 0 || movement.y < 0 || movement.y > 0)
        {
            animator.SetBool("isMoving", true);
            animator.SetBool("isIdle", false);
        }
        else
        {
            animator.SetBool("isMoving", false);
            animator.SetBool("isIdle", true);
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    public void AddSpeed(float addedSpeed)
    {
        moveSpeed += addedSpeed;
    }

    //HEALTH AND DIE
    public void TakeDamage(int damage)
    {
        if (!canMove)
            return;
        health -= damage;
        healthbar.SetHealth(health);
        if(health <= 0)
        {
            Die();
        }
    }

    public void AddHealth(int addedHealth)
    {
        maxHealth += addedHealth;
        health += (int) addedHealth/2;
    }

    void Die()
    {
        canMove = false;
        animator.SetTrigger("isDying");
        gameOverUI.SetActive(true);
        isDied = true;
    }

    //DASH
    IEnumerator Dash()
    {
        animator.SetTrigger("isDashing");

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
