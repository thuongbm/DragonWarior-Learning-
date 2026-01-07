using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowTrap : MonoBehaviour
{
    [SerializeField] private float attakCoolDown;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] arrow;
    private float coolDownTimer;

    private void attack()
    {
        coolDownTimer = 0;

        arrow[findArrow()].transform.position = firePoint.position;
        arrow[findArrow()].GetComponent<EnemyProjectTile>().ActivateProjectile();

    }

    private int findArrow()
    {
        for (int i = 0; i < arrow.Length; i++)
        {
            if (!arrow[i].activeInHierarchy)
            {
                return i;
            }
        }

        return 0;
    }

    private void Update()
    {
        coolDownTimer += Time.deltaTime;

        if (coolDownTimer >= attakCoolDown)
        {
            attack();
        }
    }
}

