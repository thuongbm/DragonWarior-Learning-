using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartCollectible : MonoBehaviour
{
   [SerializeField] private float value;
   
   [Header("SFX")]
   [SerializeField] private AudioClip heartClaimSound;
   
   private void OnTriggerEnter2D(Collider2D other)
   {
      if (other.tag == "Player")
      {
         SoundManager.instance.PlaySound(heartClaimSound);
         other.GetComponent<Health>().AddHealth(value);
         gameObject.SetActive(false);
      }
   }
}
