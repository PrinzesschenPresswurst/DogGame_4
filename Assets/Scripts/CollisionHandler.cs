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

    //[SerializeField] private ParticleSystem winParticle;

    private AudioSource audioSource;
    private MeshRenderer meshRenderer;

    private int currentSceneIndex;
    private int nextSceneIndex;
    private int totalSceneAmount;
    private int startSceneIndex;
    bool crashSoundHasPlayed = false;
    
    public bool isTransitioning;

    private void Start()
    {
        startSceneIndex = 0;
        totalSceneAmount = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        audioSource = GetComponent<AudioSource>();
        isTransitioning = false;
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
        DisableControls();
        audioSource.PlayOneShot(winSound);
        //winParticle.Play();
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
}
