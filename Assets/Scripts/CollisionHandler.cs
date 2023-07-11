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

    private void Start()
    {
        canMove = true;
    }

    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log(other.gameObject.tag);
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
    
    void CrashFeedback()
    {
        //GetComponent<Rigidbody>().isKinematic = true;
        canMove = false;
        GetComponent<MeshRenderer>().material = crashMaterial;
        Debug.Log("Damn you exploded, try again...");
    }
    
    void Respawn()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }

    void LevelCompleteFeedback()
    {
        canMove = false;
        Debug.Log("Well done, loading next level...");
    }
    
    void LoadNextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        SceneManager.LoadScene(nextSceneIndex);
    }
}
