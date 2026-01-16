using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Robot : MonoBehaviour
{
    [Header("Attack Parameters")]
    [SerializeField] private float damage;
    [SerializeField] private float attackCoolDown;
    [SerializeField] private float range;

    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    
    private Animator animator;
    private Health health;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        
        if (PlayerInSight())
        {
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                animator.SetTrigger("Shooting");
            }
        }
    }

    private bool PlayerInSight()
    {
        Vector3 boxOrigin = boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        
        RaycastHit2D hit = Physics2D.BoxCast(boxOrigin, boxSize, 0, Vector2.left, 0, playerLayer);
        if (hit.collider != null)
        {
            health = hit.collider.GetComponent<Health>();
        }
        
        return hit.collider != null;
    }
    
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        
        Vector3 boxOrigin = boxCollider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z);
        
        Gizmos.DrawCube(boxOrigin, boxSize);
    }

    private void DamagePlayer()
    {
        if (PlayerInSight())
        {
            health.takeDamage(damage);
        }
    }
}
