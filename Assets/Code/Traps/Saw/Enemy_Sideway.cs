using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Sideway : MonoBehaviour
{
   [SerializeField] private float speed;
   [SerializeField] private float damage;
   [SerializeField] private float movermentDistance;
   
   private bool moveLeft;
   private float leftEgde;
   private float rightEgde;

   private void Awake()
   {
      leftEgde = transform.position.x - movermentDistance;
      rightEgde = transform.position.x + movermentDistance;
   }

   private void Update()
   {
      if (moveLeft)
      {
         if (transform.position.x > leftEgde)
         {
            transform.position = new Vector3(transform.position.x - speed * Time.deltaTime, transform.position.y, transform.position.z);
         }
         else
         {
            moveLeft = false;
         }
      }
      else
      {
         if (transform.position.x < rightEgde)
         {
            transform.position = new Vector3(transform.position.x + speed * Time.deltaTime, transform.position.y, transform.position.z);
         }
         else
         {
            moveLeft = true;  
         }
      }
   }

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         other.GetComponent<Health>().takeDamage(damage);
      }
   }
}
