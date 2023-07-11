using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostSoundHandler : MonoBehaviour
{
    private AudioSource audioSource;
    [SerializeField] private AudioClip boostSound;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource.clip = boostSound;
    }

    private void Update()
    {
        PlayBoostSound();
    }

    void PlayBoostSound()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            audioSource.loop = true;
            audioSource.Play();
            
        }
        else if (Input.GetKeyUp(KeyCode.Space))
        {
            audioSource.Stop();
            audioSource.loop = false;  
        }
        
        else if (FindObjectOfType<CollisionHandler>().canMove == false)
        {
            audioSource.Stop(); 
        }
            
    }
}
