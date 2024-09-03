using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void PlayOriginal()
    {
        SceneManager.LoadSceneAsync("DinoGame");
    }

    public void PlayAlternate()
    {
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
