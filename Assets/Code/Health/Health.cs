using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Health : MonoBehaviour
{
    [Header("Health")]
    [SerializeField] private float startHealth;
    public float currentHealth {get; private set;}
    private Animator animator;
    private bool dead;
    
    
    [Header("IFrame")]
    [SerializeField] private float iFrameDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRenderer;
    private void Awake()
    {
        currentHealth = startHealth;
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void takeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, startHealth);

        if (currentHealth > 0)
        {
            animator.SetTrigger("Hurt");

            StartCoroutine(Invulnerability());
        }
        else
        {
            if (!dead)
            {
                animator.SetTrigger("Die");
                //player
                if (GetComponent<moverment>() != null)
                {
                    GetComponent<moverment>().enabled = false;
                }

                //knight
                if (GetComponentInParent<KnightPatrol>() != null)
                {
                    GetComponentInParent<KnightPatrol>().enabled = false;
                }

                if (GetComponent<Knight>() != null)
                {
                    GetComponent<Knight>().enabled = false;
                }
                dead = true;
            }
        }
    }

    public void AddHealth(float value)
    {
        currentHealth = Mathf.Clamp(currentHealth + value, 0, startHealth);
    }

    private IEnumerator Invulnerability()
    {
        Physics2D.IgnoreLayerCollision(11, 12, true);

        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRenderer.color = new Color(1, 0, 0, 0.5f);
            
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
            
            spriteRenderer.color = Color.white;
            
            yield return new WaitForSeconds(iFrameDuration / (numberOfFlashes * 2));
        }
        
        Physics2D.IgnoreLayerCollision(11, 12, false);
    }
}
