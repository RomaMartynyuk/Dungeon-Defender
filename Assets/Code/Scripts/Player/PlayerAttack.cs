using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
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

    void Update()
    {
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
