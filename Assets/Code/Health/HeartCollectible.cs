using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
   [SerializeField] private float value;

   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         other.GetComponent<Health>().AddHealth(value);
         gameObject.SetActive(false);
      }
   }
}
