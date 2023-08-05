using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator animator;
    Swordman player;

    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float stopDistance = 0.5f;
    Transform targetPlayer;
    bool facingRight = true;
    bool canMove = true;

    [Header("Health")]
    [SerializeField] int maxHealth = 10;
    int health;
    Healthbar healthbar;

    [Header("Attack")]
    [SerializeField] int damage = 5;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float timeBtwAttacks;
    float _timeBtwAttacks;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        healthbar = GetComponentInChildren<Healthbar>();
    }

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Swordman>();

        health = maxHealth;
        _timeBtwAttacks = timeBtwAttacks;

        healthbar.SetMaxHealth(health);
    }

    void Update()
    {
        if (!canMove | player.isDied)
            return;
        if (targetPlayer == null)
            return;

        if (Vector2.Distance(transform.position, targetPlayer.transform.position) < attackRange)
        {
            Attack();
        }
        else if(Vector2.Distance(transform.position, targetPlayer.transform.position) > attackRange)
        {
            Move();
        }
    }

    void FixedUpdate()
    {
        if (targetPlayer.position.x < transform.position.x && facingRight)
        {
            Flip();
        }
        else if (targetPlayer.position.x > transform.position.x && !facingRight)
        {
            Flip();
        }
    }

    void Move()
    {
        if (Vector2.Distance(transform.position, targetPlayer.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isIdling", false);
            animator.SetBool("isMoving", true);
            _timeBtwAttacks = timeBtwAttacks;
        }
    }

    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }

    void Attack()
    {
        animator.SetBool("isMoving", false);
        animator.SetTrigger("isAttacking");
        if (_timeBtwAttacks <= 0)
        {
            targetPlayer.GetComponent<Swordman>().TakeDamage(damage);
            _timeBtwAttacks = timeBtwAttacks;
        }
        else
        {
            animator.SetBool("isIdling", true);
            _timeBtwAttacks -= Time.deltaTime;
        }
    }

    public void TakeDamage(int damage)
    {
        if(canMove)
        {
            health -= damage;
            healthbar.SetHealth(health);
            if (health <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        canMove = false;
        animator.SetTrigger("isDying");
        FindObjectOfType<WaveManager>().DecreaseAliveEnemies();
        GetComponent<Collider2D>().enabled = false;
        Destroy(gameObject, 1f);
    }
}
