using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;

    public void PlayOriginal()
    {
        SceneManager.LoadSceneAsync("DinoGame");
        if(audioSource != null)
        {
            Debug.LogError("here");
            audioSource.Play();
        }
    }

    public void PlayAlternate()
    {
        SceneManager.LoadSceneAsync("AlternateGame");
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void CreditsPage()
    {
        SceneManager.LoadSceneAsync("Credits");
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

    public void VersionNotesPage()
    {
        SceneManager.LoadSceneAsync("VersionNotes");
        if(audioSource != null)
        {
            audioSource.Play();
        }
    }

}
