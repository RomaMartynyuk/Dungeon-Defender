using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    Animator animator;
    Swordman swordman;

    [SerializeField] Transform attackPoint;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] LayerMask enemyLayers;
    [SerializeField] int damage = 5;
    [SerializeField] float reloadTime;
    float _reloadTime;

    void Start()
    {
        _reloadTime = reloadTime;
    }

    private void Start()
    {
        animator = GetComponent<Animator>();
        swordman = GetComponent<Swordman>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && _reloadTime <= 0)
        {
            StartCoroutine(Attack());
        }
        else
        {
            _reloadTime -= Time.deltaTime;
        }
    }

    IEnumerator Attack()
    {
        swordman.canMove = false;
        animator.SetBool("isIdle", false);
        animator.SetBool("isMoving", false);
        animator.SetTrigger("isAttacking");

        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, enemyLayers);

        foreach(Collider2D enemy in hitEnemies)
        {
            enemy.GetComponent<Zombie>().TakeDamage(damage);
        }

        _reloadTime = reloadTime;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
