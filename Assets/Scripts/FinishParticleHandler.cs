using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishParticleHandler : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag == "Player" && FindObjectOfType<ScoreCounter>().allTreatsCollected)
        {
            GetComponent<ParticleSystem>().Play();
        }
    }
}
