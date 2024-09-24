using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public AudioSource audioSource;
    public static bool altGame;

    public void PlayOriginal()

    {
	altGame = false;
        SceneManager.LoadSceneAsync("DinoGame");
    }

    public void PlayAlternate()
    {
	altGame = true;
        SceneManager.LoadSceneAsync("AlternateGame");
    }

    public void CreditsPage()
    {
        SceneManager.LoadSceneAsync("Credits");
    }

    public void VersionNotesPage()
    {
        SceneManager.LoadSceneAsync("VersionNotes");
    }

}
