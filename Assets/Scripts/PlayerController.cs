using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float rotateSpeed = 100f;
    [SerializeField] float thrustAmount = 1000f;
    private Rigidbody rb;
    

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (FindObjectOfType<CollisionHandler>().canMove == true)
        {
            ProcessRotation();
            ProcessBoost();
        }
    }
    
    void ProcessBoost()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            float thrust = thrustAmount * Time.deltaTime;
            rb.AddRelativeForce(Vector3.up * thrust);
        }
    }

    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            CalculateRotation(rotateSpeed);
        }
        
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            CalculateRotation(- rotateSpeed);
        }
    }
    private void CalculateRotation(float containerForRotateSpeed)
    {
        rb.freezeRotation = true; // so the physics system is not messing with the manual rotation
        transform.Rotate(Vector3.forward * containerForRotateSpeed * Time.deltaTime);
        rb.freezeRotation = false;
    }
}

