using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float startHealth;
    public float currentHealth {get; private set;}
    private Animator animator;
    private bool dead;
    
    private void Awake()
    {
        currentHealth = startHealth;
        animator = GetComponent<Animator>();
    }

    public void takeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("Die");
                GetComponent<moverment>().enabled = false;
                dead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startHealth);
    }
}
