using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Damage : MonoBehaviour
{
    [SerializeField] float damage;

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            other.GetComponent<Health>().takeDamage(damage);
        }
    }
}
