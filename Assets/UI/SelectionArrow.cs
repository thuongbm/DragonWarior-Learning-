using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectionArrow : MonoBehaviour
{
    [SerializeField] private AudioClip changeSound;
    [SerializeField] private AudioClip interactSound;
    [SerializeField] private RectTransform[] buttons;
    private RectTransform rect;
    private int currentPosition;

    private void Awake()
    {
        rect = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
        {
            ChangePosition(-1);
        }

        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
        {
            ChangePosition(1);
        }
    }

    private void ChangePosition(int position)
    {
        currentPosition += position;

        if (position != 0)
        {
            SoundManager.instance.PlaySound(changeSound);
        }

        if (currentPosition < 0)
        {
            currentPosition = buttons.Length - 1;
        }
        else if (currentPosition > buttons.Length - 1)
        {
            currentPosition = 0;
        }
        
        rect.position = new Vector3(rect.position.x, buttons[currentPosition].position.y, 0);
    }
}
