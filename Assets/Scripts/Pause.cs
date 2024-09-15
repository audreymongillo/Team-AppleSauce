using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;  // Add this to access EventSystem

public class Pause : MonoBehaviour
{
    private bool isPaused = false;
    public TextMeshProUGUI buttonText;

    public void TogglePause()
    {
        if (isPaused)
        {
            ResumeGame();
            buttonText.text = "Pause";
        }
        else
        {
            PauseGame();
            buttonText.text = "Resume";
        }

        EventSystem.current.SetSelectedGameObject(null);
    }

    private void PauseGame()
    {
        Time.timeScale = 0f;
        isPaused = true;
    }

    private void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }
}

