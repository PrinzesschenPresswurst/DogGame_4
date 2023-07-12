using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine.SceneManagement;
using UnityEngine;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private Material crashMaterial;
    public bool canMove;
    private int currentSceneIndex;
    private int nextSceneIndex;
    private int totalSceneAmount;
    private int startSceneIndex;

    private void Start()
    {
        canMove = true;
        startSceneIndex = 0;
        totalSceneAmount = SceneManager.sceneCountInBuildSettings;
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case "Friendly":
                Debug.Log("no problem");
                break;
            case "Finish":
                LevelCompleteFeedback();
                Invoke("LoadNextLevel",1f);
                break;
            default:
                CrashFeedback();
                Invoke("Respawn",1f);
                break;
        }
    }
    
    //CRASH CASE
    void CrashFeedback()
    {
        canMove = false;
        GetComponent<MeshRenderer>().material = crashMaterial;
        Debug.Log("Damn you exploded, try again...");
    }
    
    void Respawn()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    // LEVEL COMPLETE CASE
    void LevelCompleteFeedback()
    {
        canMove = false;
        Debug.Log("Well done, loading next level...");
    }

    void LoadNextLevel()
    {
        if (currentSceneIndex == totalSceneAmount - 1)
        {
            SceneManager.LoadScene(startSceneIndex);
        }
        
        else SceneManager.LoadScene(nextSceneIndex);
    }
}
