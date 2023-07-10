using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieMove : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float stopDistance;
    Transform targetPlayer;

    bool facingRight = true;

    void Start()
    {
        targetPlayer = GameObject.FindGameObjectWithTag("Player").GetComponent<Transform>();
    }

    void Update()
    {
        if (Vector2.Distance(transform.position, targetPlayer.position) > stopDistance)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPlayer.position, moveSpeed * Time.deltaTime);
        }
    }

    void FixedUpdate()
    {
        if (targetPlayer.position.x < 0 && facingRight)
        {
            Flip();
        }
        else if (targetPlayer.position.x > 0 && !facingRight)
        {
            Flip();
        }
    }
    void Flip()
    {
        facingRight = !facingRight;
        transform.Rotate(0f, 180f, 0f);
    }
}
