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
    
    [Header("Behavior")]
    [SerializeField] private Behaviour[] behaviours;
    
    [Header("Death Sound")]
    [SerializeField] private AudioClip deathSound;
    
    [Header("Hurt Sound")]
    [SerializeField] private AudioClip hurtSound;
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
            
            SoundManager.instance.PlaySound(hurtSound);
        }
        else
        {
            if (!dead)
            {
                foreach (Behaviour behaviour in behaviours)
                {
                    behaviour.enabled = false;
                }
                
                animator.SetBool("isGrounded", false);
                animator.SetTrigger("Die");
                
                dead = true;
                
                SoundManager.instance.PlaySound(deathSound);
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
