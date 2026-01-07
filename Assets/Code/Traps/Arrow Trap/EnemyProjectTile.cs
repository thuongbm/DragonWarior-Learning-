using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProjectTile : Enemy_Damage
{
    [SerializeField] private float timeReset;
    [SerializeField] private float speed;
    private float lifeTime;

    public void ActivateProjectile()
    {
        lifeTime = 0;
        
        gameObject.SetActive(true);
    }

    private void Update()
    {
        float movermentSpeed = speed * Time.deltaTime;
        transform.Translate(movermentSpeed, 0, 0);
        
        lifeTime += Time.deltaTime;
        if (lifeTime >= timeReset)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        gameObject.SetActive(false);
    }
}
