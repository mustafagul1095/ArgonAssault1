using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollusionHandler : MonoBehaviour
{
    [SerializeField] private GameObject crashVFX;
    private void OnTriggerEnter(Collider other)
    {
        StartCrashSequence();
    }

    private void StartCrashSequence()
    {
        //crashVFX.Play();
        Instantiate(crashVFX, transform.position, Quaternion.identity);
        GetComponent<PlayerControls>().enabled = false;
        Invoke(nameof(ReloadLevel), 1f);
    }

    private void ReloadLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
    }
}
