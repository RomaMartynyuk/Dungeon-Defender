using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] float moveSpeed;
    [SerializeField] float stopDistance = 0.5f;
    Transform targetPlayer;
    bool facingRight = true;

    [Header("Health")]
    [SerializeField] int maxHealth = 10;
    int health;

    [Header("Attack")]
    [SerializeField] int damage = 5;
    [SerializeField] float attackRange = 1f;
    [SerializeField] float timeBtwAttacks;
    float _timeBtwAttacks;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        health = maxHealth;
        _timeBtwAttacks = timeBtwAttacks;
    }

    void Update()
    {
        if (targetPlayer == null)
            return;

        if(Vector2.Distance(transform.position, targetPlayer.transform.position) < attackRange)
        {
            Attack();
        }
        else
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
        if(_timeBtwAttacks <= 0)
        {
            targetPlayer.GetComponent<Swordman>().TakeDamage(damage);
            _timeBtwAttacks = timeBtwAttacks;
        }
        else
        {
            _timeBtwAttacks -= Time.deltaTime;
        }
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
        Destroy(gameObject);
    }
}
