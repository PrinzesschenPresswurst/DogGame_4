using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Material crashMaterial;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private ParticleSystem crashParticle;
    [SerializeField] private AudioClip winSound;

    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    private int currentSceneIndex;
    private int nextSceneIndex;
    private int totalSceneAmount;
    private int startSceneIndex;
    bool crashSoundHasPlayed = false;
    
    private bool isTransitioning = false;
    private bool collisionOff = false;

    private void Start()
    {
        startSceneIndex = 0;
        totalSceneAmount = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
    }

    private void Update()
    {
        Cheat_LoadNextLevel();
        Cheat_NoCollisions();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (isTransitioning) { return; }
        
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("no problem");
                break;
            case "Finish":
                LevelCompleteSequence();
                break;
            default:
                if (collisionOff)
                {
                    other.gameObject.GetComponent<Collider>().enabled = false;
                    return;
                }
                CrashSequence();
                break;
        }
    } 
    
//CRASH CASE
    void CrashSequence()
    {
        DisableControls();
        if (!crashSoundHasPlayed)
        {
            audioSource.PlayOneShot(crashSound);
            crashSoundHasPlayed = true;
        }

        GetComponentInChildren<MeshRenderer>().material = crashMaterial;
        crashParticle.Play();
        Invoke("Respawn",1f);
    }
    
    void Respawn()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }
    
// LEVEL COMPLETE CASE
    void LevelCompleteSequence()
    {
        if (FindObjectOfType<ScoreCounter>().allTreatsCollected == false)
        {
            return;
        }
        DisableControls();
        audioSource.PlayOneShot(winSound);
        Invoke("LoadNextLevel",1f);
    }

    void LoadNextLevel()
    {
        if (currentSceneIndex == totalSceneAmount - 1)
        {
            SceneManager.LoadScene(startSceneIndex);
        }
        
        else SceneManager.LoadScene(nextSceneIndex);
    }

    void DisableControls()
    {
        isTransitioning = true;
        audioSource.Stop();
        GetComponentInChildren<ParticleSystem>().Stop();
        GetComponentInChildren<ParticleSystem>().Clear();
        GetComponent<PlayerController>().enabled = false;
        
    }

    void Cheat_LoadNextLevel()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
    }

    void Cheat_NoCollisions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            collisionOff = !collisionOff;
            //GetComponent<Collider>().enabled = false;
        }
    }
}
