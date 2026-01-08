using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpikeHead : Enemy_Damage
{
    [SerializeField] private float speed;
    [SerializeField] private float range;
    [SerializeField] private float checkDelay;
    [SerializeField] private LayerMask layerPlayer;
    
    private Vector3[] directions = new Vector3[4];
    private Vector3 destination;
    private float checkTimer;
    private bool attacking;

    private void OnEnable()
    {
        Stop();
    }

    private void Update()
    {
        if (attacking)
        {
            transform.Translate(destination * speed * Time.deltaTime);
        }
        else
        {
            checkTimer += Time.deltaTime;
            if (checkTimer >= checkDelay)
            {
                CheckForPlayer();
            }
        }
        
    }

    private void CheckForPlayer()
    {
        CalculateDirection();

        for (int i = 0; i < directions.Length; i++)
        {
            RaycastHit2D raycastHit2D = Physics2D.Raycast(transform.position, directions[i], range, layerPlayer);

            if (raycastHit2D.collider != null && !attacking)
            {
                destination = directions[i];
                attacking = true;
                checkTimer = 0;
            }
        }
    }

    private void CalculateDirection()
    {
        directions[0] = transform.right * range;
        directions[1] = -transform.right * range;
        directions[2] = transform.up * range;
        directions[3] = -transform.up * range;
    }

    private void Stop()
    {
        destination = transform.position;
        attacking = false;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        Stop();
    }
}
