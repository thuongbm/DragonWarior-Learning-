using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletAni : MonoBehaviour
{
    [Header("Bullet")]
    private BoxCollider2D boxCollider;
    private Animator animator;
    
    [Header("Bullet parameters")]
    [SerializeField] float speed;
    [SerializeField] float damage;
    private bool hit;
    private float direction;
    
    private void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (hit) return;
        
        float movermentSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movermentSpeed, 0, 0);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        hit = true;
        boxCollider.isTrigger = false;
        animator.SetTrigger("Explode");
    }
    
    
}
