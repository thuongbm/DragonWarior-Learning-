using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnightPatrol : MonoBehaviour
{
    [Header("Side patrol")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    
    [Header("Knight")]
    [SerializeField] private Transform Knight;
    
    [Header("Idle Behavior")]
    [SerializeField] private float idleDuration;
    private float idleTimer;
    
    [Header("Moverment parameters")]
    [SerializeField] private float speed;
    [SerializeField] private Animator animator;
    
    private bool movingLeft;
    private Vector3 initScale;

    private void Awake()
    {
        initScale = transform.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            if (Knight.position.x >= leftEdge.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
        }
        else if (!movingLeft)
        {
            if (Knight.position.x <= rightEdge.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }
        }
    }

    private void DirectionChange()
    {
        animator.SetBool("Moving", false);
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }
    private void OnDisable()
    {
        animator.SetBool("Moving", false);
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        
        animator.SetBool("Moving", true);
        
        Knight.localScale = new Vector3(Mathf.Abs(Knight.localScale.x) * direction, Knight.localScale.y, Knight.localScale.z);
        
        Knight.position = new Vector3(Knight.position.x + direction * speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
