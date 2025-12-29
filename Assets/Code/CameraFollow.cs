using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;
    [SerializeField] private float CameraSpeed;
    [SerializeField] private float CameraDistance;
    private float lookAhead;
    
    void Update()
    {
        transform.position = new Vector3(player.position.x + lookAhead, transform.position.y, transform.position.z); 
        
        lookAhead = Mathf.Lerp(lookAhead, (CameraDistance * player.localScale.x), CameraSpeed * Time.deltaTime);
    }
}
