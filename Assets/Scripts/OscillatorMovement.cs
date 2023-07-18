using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OscillatorMovement : MonoBehaviour
{
    private Vector3 startPosition;
    [SerializeField] private Vector3 movementVector;
    [SerializeField] [Range(0,1)] private float movementFactor;
    [SerializeField] private float period = 2f;

    void Start()
    {
        startPosition = transform.position;
        //Debug.Log(startPosition);
    }
    
    void Update()
    {
        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2;
        float rawSinWave = Mathf.Sin(cycles * tau);

        movementFactor = (rawSinWave + 1f) / 2f;
        Vector3 offset = movementVector * movementFactor;
        transform.position = startPosition + offset;
    }
}
