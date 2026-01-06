using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireTrap : MonoBehaviour
{
    [SerializeField] float damage;
    [SerializeField] private float activationDelay;
    [SerializeField] private float activeTime;
    private Animator animator;
    private SpriteRenderer spriteRenderer;

    private bool active;
    private bool triggered;
    
    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            if (!triggered)
            {
                StartCoroutine(ActiveFireTrap());
            }

            if (active)
            {
                other.GetComponent<Health>().takeDamage(damage);
            }
        }
    }

    private IEnumerator ActiveFireTrap()
    {
        triggered = true;
        spriteRenderer.color = Color.red;
        
        yield return new WaitForSeconds(activationDelay);
        spriteRenderer.color = Color.white;
        active = true;
        animator.SetBool("Activated", true);
        
        yield return new WaitForSeconds(activeTime);
        active = false;
        triggered = false;
        animator.SetBool("Activated", false);
    }
}
