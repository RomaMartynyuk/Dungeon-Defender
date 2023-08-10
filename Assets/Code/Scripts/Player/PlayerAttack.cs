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

    private void Awake()
    {
        animator = GetComponent<Animator>();
        swordman = GetComponent<Swordman>();
    }

    void Start()
    {
        _reloadTime = reloadTime;
    }

    void Update()
    {
        if (!swordman.canMove)
            return;
        if (Input.GetMouseButtonDown(0) && _reloadTime <= 0)
        {
            Attack();
        }
        else
        {
            _reloadTime -= Time.deltaTime;
        }
    }

    void Attack()
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
        swordman.canMove = true;
    }

    public void AddDamage(int addedDamage)
    {
        damage += addedDamage;
    }

    void OnDrawGizmosSelected()
    {
        if (attackPoint == null)
            return;

        Gizmos.DrawWireSphere(attackPoint.position, attackRange);
    }
}
