using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VersionNotesScreen : MonoBehaviour
{
    public void BackButtons()
    {
        SceneManager.LoadSceneAsync("StartScreen");
    }
}
