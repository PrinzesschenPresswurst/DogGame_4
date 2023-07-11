using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObstacleMover : MonoBehaviour
{
    [SerializeField] private float obstacleMoveSpeed = 1f;
    [SerializeField] private float obstacleMoveDistance = 5f;
    
    private bool directionBackwards = true;

    void Update () {
        if (directionBackwards)
        {
            transform.Translate (Vector3.forward * obstacleMoveSpeed * Time.deltaTime);
        }

        else
        {
            transform.Translate (-Vector3.forward * obstacleMoveSpeed * Time.deltaTime); 
        }
        
        if(transform.position.z >= obstacleMoveDistance) 
        {
            directionBackwards = false;
        }
		
        if(transform.position.z <= -obstacleMoveDistance) 
        {
            directionBackwards = true;
        } 
    } 
}


