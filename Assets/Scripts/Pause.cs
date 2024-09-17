using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;

public class Pause : MonoBehaviour
{
    public static bool isPaused = false;
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

    public static void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;
    }

}
