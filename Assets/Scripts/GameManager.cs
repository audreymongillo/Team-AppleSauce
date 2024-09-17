using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public float initialGameSpeed = 5f;
    public float gameSpeedIncrease = 0.1f;
    public float gameSpeed { get; private set; }

    public TextMeshProUGUI gameOverText;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI hiscoreText;

    public Button retryButton;
    public ConfettiManager confettiManager = null;

    private Player player;
    private Spawner spawner;
    public float score;
    public static float currentScore;

    private const string FirstRunKey = "FirstRun";
    private bool isSlowedDown = false; // Flag for tracking if slowdown is active

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            DestroyImmediate(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    private void Start()
    {
        Pause.ResumeGame();
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        if (confettiManager == null)
        {
            Debug.LogError("ConfettiManager is not assigned in GameManager.");
        }

        // Check if this is the first run
        if (PlayerPrefs.GetInt(FirstRunKey, 0) == 0)
        {
            // First time opening the game, initialize settings
            PlayerPrefs.SetFloat("hiscore", 0);
            PlayerPrefs.SetInt(FirstRunKey, 1); // Set flag to indicate game has been opened before
            PlayerPrefs.Save();
        }

        NewGame();
    }

    public void NewGame()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();

        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }

        score = 0f;
        gameSpeed = initialGameSpeed;

        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        UpdateHiscore();





    }

    private void Update()
    {
        if (!isSlowedDown)
        {
            gameSpeed += gameSpeedIncrease * Time.deltaTime;
        }
        
        score += gameSpeed * Time.deltaTime;
        scoreText.text = Mathf.FloorToInt(score).ToString("D5");
	currentScore = score;
	//SwitchBackGround();

    }

    public void GameOver()
    {
        StopCoroutine("SlowDownCoroutine");

        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
        
        UpdateHiscore();
    }

    public void SlowDown()
<<<<<<< Updated upstream
    {
        if (!isSlowedDown)
        {
            StartCoroutine(SlowDownCoroutine());
        }
    }
=======
	{
		float newSpeed = gameSpeed;
		
		
	
		gameSpeed = 5f;
		
		
	
>>>>>>> Stashed changes

    private IEnumerator SlowDownCoroutine()
    {
        isSlowedDown = true;
        float originalSpeed = gameSpeed;
        gameSpeed = originalSpeed * (2f / 3f);

        // Set the jump force to be slightly higher during slowdown
        player.SetJumpForce(player.defaultJumpForce * 1.2f); // Adjust multiplier as needed

        yield return new WaitForSecondsRealtime(5f);

        gameSpeed = originalSpeed;
        player.SetJumpForce(player.defaultJumpForce);
        isSlowedDown = false;
    }

    public void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
            PlayerPrefs.Save(); // Save changes to PlayerPrefs

<<<<<<< Updated upstream
            // Play confetti effect when a new high score is achieved
            if (confettiManager != null)
            {
                Debug.Log("New high score! Triggering confetti.");
                confettiManager.PlayConfetti();
            }
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
=======
			// Play confetti effect when a new high score is achieved
			if (confettiManager != null)
			{
				Debug.Log("New high score! Triggering confetti.");
				confettiManager.PlayConfetti();
			}
		}
		hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");





	}


	



>>>>>>> Stashed changes
}
