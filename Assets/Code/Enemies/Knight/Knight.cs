using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knight : MonoBehaviour
{
    [Header ("Attack Parameters")]
    [SerializeField] private float damage;
    [SerializeField] private float range;
    [SerializeField] private float attackCoolDown;
    
    [Header("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D collider;
    
    [Header("Player")]
    [SerializeField] private LayerMask playerLayer;
    private float coolDownTimer = Mathf.Infinity;
    
    private Animator animator;
    private Health health;
    private KnightPatrol knightPatrol;

    private void Awake()
    {
        animator = GetComponent<Animator>();
        knightPatrol = transform.parent.GetComponentInChildren<KnightPatrol>();
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;
        
        if (PlayerInSight())
        {
            if (coolDownTimer >= attackCoolDown)
            {
                coolDownTimer = 0;
                animator.SetTrigger("triggerAtk");
            }
            
        }

        if (knightPatrol != null)
        {
            knightPatrol.enabled = !PlayerInSight();
        }
    }

    private bool PlayerInSight()
    {
        Vector3 boxOrigin = collider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(collider.bounds.size.x * range, collider.bounds.size.y, collider.bounds.size.z);
        
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
        
        Vector3 boxOrigin = collider.bounds.center + transform.right * range * transform.localScale.x * colliderDistance;
        Vector3 boxSize = new Vector3(collider.bounds.size.x * range, collider.bounds.size.y, collider.bounds.size.z);
        
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
