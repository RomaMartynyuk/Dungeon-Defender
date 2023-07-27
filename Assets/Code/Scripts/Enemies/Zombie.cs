using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    Animator animator;
    bool canMove = true;

    [Header("Movement")]
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    Transform targetPlayer;
    bool facingRight = true;

    [Header("Health")]
    [SerializeField] int maxHealth = 10;
    int health;

    void Start()
    {
        animator = GetComponent<Animator>();

        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();

        health = maxHealth;
    }

    void Update()
    {
        if (!canMove)
        { return; }

        if (targetPlayer == null)
            return;
        if (Vector2.Distance(transform.position, targetPlayer.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
            animator.SetBool("isMoving", true);
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
            StartCoroutine(Die());
        }
    }

    IEnumerator Die()
    {
        canMove = false;
        animator.SetTrigger("isDying");
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }
}
