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
    
    [SerializeField] private float speed = 5f;
    [SerializeField] private LayerMask groundLayer;
    [SerializeField] private LayerMask wallLayer;
    
    [Header("Jump power")]
    [SerializeField] private float jumpPower;
    
    [Header("Extra Jump")]
    [SerializeField] private int extraJump;
    private int extraJumpCounter;
    
    [Header("SFX")]
    [SerializeField] private AudioClip jumpSound;
    
    [Header("Coyote time")]
    [SerializeField] private float coyoteTime;
    private float coyoteCounter;
    
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

        if (horizontalInput > 0.01f)
        {
            transform.localScale = Vector3.one;
        }
        else if (horizontalInput < 0.01f)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
            
        }

        if (Input.GetKeyUp(KeyCode.Space) && body.velocity.y > 0) 
        {
            body.velocity = new Vector2(body.velocity.x, body.velocity.y / 2);
        }

        if (onWall())
        {
            body.gravityScale = 0;
            body.velocity = Vector2.zero;
        }
        else
        {
            body.gravityScale = 1;
            body.velocity = new Vector2(horizontalInput * speed, body.velocity.y);

            if (isGrounded())
            {
                coyoteCounter = coyoteTime;
                extraJumpCounter = extraJump;
            }
            else
            {
                coyoteCounter -= Time.deltaTime;
            }
        }
    }

    private void Jump()
    {
        if (coyoteCounter < 0 && !onWall() && extraJumpCounter <= 0)
        {
            return;
        }
        
        SoundManager.instance.PlaySound(jumpSound);

        if (onWall())
        {
            WallJump();
        }
        else if (isGrounded())
        {
            body.velocity = new Vector2(body.velocity.x, jumpPower);
        }
        else
        {
            if (coyoteCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
            }
            else if (extraJumpCounter > 0)
            {
                body.velocity = new Vector2(body.velocity.x, jumpPower);
                extraJumpCounter--;
            }
        }

        coyoteCounter = 0;

    }

    private void WallJump()
    {
        
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
