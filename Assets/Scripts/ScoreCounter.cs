using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
    [SerializeField] private AudioClip treatCollectSound;
    private int score;
    private AudioSource audioSource;
    public bool allTreatsCollected = false;

    private void Start()
    {
        score = 0;
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Treat")
        {
            audioSource.PlayOneShot(treatCollectSound);
            Destroy(other.gameObject);
            score++;
            Debug.Log("treats collected: " +score);
        }

        if (score == 5)
        {
            allTreatsCollected = true;
            Debug.Log("all treats collected");
        }
    }
}
