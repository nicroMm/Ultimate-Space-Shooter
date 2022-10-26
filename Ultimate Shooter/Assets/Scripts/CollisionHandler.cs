using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float reloadDelay = 1f;
    [SerializeField] ParticleSystem crashFX;
    [SerializeField] AudioClip crashSFX;
    AudioSource audioSource;
        void Start() 
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnTriggerEnter(Collider other) 
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        audioSource.PlayOneShot(crashSFX);
        crashFX.Play();
        GetComponent<MeshRenderer>().enabled = false;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<PlayerControler>().enabled = false;
        Invoke("ReloadLevel", reloadDelay);
    }

    void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
