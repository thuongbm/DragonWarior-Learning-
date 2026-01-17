using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RobotProjectile : MonoBehaviour
{
    private BoxCollider2D boxCollider;
    private Animator animator;
    
    [SerializeField] float speed;
    private bool hit;
    private float direction;
    
    void Awake()
    {
        boxCollider = GetComponent<BoxCollider2D>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (hit) return;
        
        float movermentSpeed = speed * Time.deltaTime * direction;
        transform.Translate(movermentSpeed, 0, 0);
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        hit = true;
        boxCollider.enabled = false;
        animator.SetTrigger("Explode");
    }
    
    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;
        
        float localScaleX = transform.localScale.x;

        if (Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }
        
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

    private void Deactive()
    {
        gameObject.SetActive(false);
    }
}
