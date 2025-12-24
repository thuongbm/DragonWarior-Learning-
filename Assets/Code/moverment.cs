using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moverment : MonoBehaviour
{
    private Rigidbody2D body;
    private Animator animator;
    private bool Grounded;
    private BoxCollider2D box;
    private float wallJumpCoolDown;
    private float horizontalInput;
    
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private LayerMask groundLayer;
    [SerializeField]
    private LayerMask wallLayer;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        box = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        
        animator.SetBool("IsRunning", horizontalInput != 0);
        animator.SetBool("IsGrounded", isGrounded());

        if (wallJumpCoolDown > 0.2f)
        {
            if (horizontalInput > 0.01f)
            {
                transform.localScale = Vector3.one;
            }
            else if (horizontalInput < -0.01f)
            {
                transform.localScale = new Vector3(-1, 1, 1);
            }
            
            body.velocity = new Vector2(Input.GetAxis("Horizontal") * speed, body.velocity.y);
            
            if (Input.GetKeyDown((KeyCode.Space)))
            {
                Jump();
            }

            if (onWall() && !isGrounded())
            {
                body.gravityScale = 0;
                body.velocity = Vector2.zero;
            }
            else
            {
                body.gravityScale = 1;
            }
        }
        else
        {
            wallJumpCoolDown += Time.deltaTime;
        }
    }

    private void Jump()
    {
        if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, speed);
            Grounded = false;   
            wallJumpCoolDown = 0;
        }

        else
        {
            if (horizontalInput == 0)
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 10, 0);
                transform.localScale = new Vector3(-Mathf.Sign(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            }

            else
            {
                body.velocity = new Vector2(-Mathf.Sign(transform.localScale.x) * 3, 6);
            }
        }
    }
    
    private bool isGrounded()
    {
        RaycastHit2D raycastHit2D = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, Vector2.down, 0.1f, groundLayer);
        return raycastHit2D.collider != null;
    }

    private bool onWall()
    {
        RaycastHit2D IsWall = Physics2D.BoxCast(box.bounds.center, box.bounds.size, 0, new Vector2(transform.localScale.x, 0), 0.1f, wallLayer);
        return IsWall.collider != null;
    }

    public bool canAttack()
    {
        return horizontalInput == 0 && isGrounded() && !onWall();
    }
}
