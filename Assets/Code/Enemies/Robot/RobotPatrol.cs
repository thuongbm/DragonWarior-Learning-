using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class RobotPatrol : MonoBehaviour
{
    [Header("Side patrol")]
    [SerializeField] private Transform leftEdge;
    [SerializeField] private Transform rightEdge;
    
    [Header("Robot")]
    [SerializeField] private Transform Robot;
    
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
            if (Robot.position.x >= leftEdge.position.x)
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
            if (Robot.position.x <= rightEdge.position.x)
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
        idleTimer += Time.deltaTime;

        if (idleTimer >= idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }
    
    private void MoveInDirection(int direction)
    {
        idleTimer = 0;
        
        Robot.localScale = new Vector3(Mathf.Abs(Robot.localScale.x) * direction, Robot.localScale.y, Robot.localScale.z);
        
        Robot.position = new Vector3(Robot.position.x + direction * speed * Time.deltaTime, transform.position.y, transform.position.z);
    }
}
