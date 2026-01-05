using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class atack : MonoBehaviour
{
    [SerializeField] private float attackCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;
    private float coolDown = Mathf.Infinity;
    
    private Animator animator;
    private moverment mover;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        mover = GetComponent<moverment>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0) && mover.canAttack() && coolDown > attackCoolDown)
        {
            Attack();
        }
        
        coolDown += Time.deltaTime;
    }

    private void Attack()
    {
        animator.SetTrigger("Attack");
        coolDown = 0;

        int index = FindFireBall();
        
        fireBalls[index].transform.position = firePoint.position;
        fireBalls[index].GetComponent<Projectile>().SetDirection(Mathf.Sign(transform.localScale.x));
    }

    private int FindFireBall()
    {
        for (int i = 0; i < fireBalls.Length; i++)
        {
            if (!fireBalls[i].activeInHierarchy)
                return i;
        }

        return 0;
    }
}
