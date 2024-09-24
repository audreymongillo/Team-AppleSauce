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
    public Button secondLifeButton;
    public AudioSource deathSound;
    public AudioSource highScoreSound;
    public ConfettiManager confettiManager = null;

    private Player player;
    private Spawner spawner;
    public float score;
    public static float currentScore;

    private const string FirstRunKey = "FirstRun";
    private bool isAlternateGame;
    private bool hasUsedExtraLife = false;
    private bool isSlowedDown = false;
    private Coroutine slowdownCoroutine = null;

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

        isAlternateGame = PlayerPrefs.GetInt("GameMode", 0) == 1;
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
        //ResetHighScore();
        Pause.ResumeGame();
        player = FindObjectOfType<Player>();
        spawner = FindObjectOfType<Spawner>();

        if (confettiManager == null)
        {
            Debug.LogError("ConfettiManager is not assigned in GameManager.");
        }

        // Initialize high scores based on game mode
        InitializeHiscore();

        NewGame();
    }

    private void InitializeHiscore()
    {
        string hiscoreKey = isAlternateGame ? "hiscore_alternate" : "hiscore_original";

        // Initialize high score if it doesn't exist
        if (!PlayerPrefs.HasKey(hiscoreKey))
        {
            PlayerPrefs.SetFloat(hiscoreKey, 0);
            PlayerPrefs.Save();
        }

        // Update the high score text for the current game mode
        hiscoreText.text = Mathf.FloorToInt(PlayerPrefs.GetFloat(hiscoreKey)).ToString("D5");
    }

    public void NewGame()
    {
        if (slowdownCoroutine != null)
        {
            StopCoroutine(slowdownCoroutine);
            slowdownCoroutine = null;
        }
        isSlowedDown = false;
        hasUsedExtraLife = false; // Reset extra life

        // Clear previous obstacles
        ClearObstacles();

        score = 0f;
        gameSpeed = initialGameSpeed;

        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true);
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        secondLifeButton.gameObject.SetActive(false);
        UpdateHiscore(); // Update high score display at the start of the new game
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
    }

    public void GameOver()
    {
	float hiscore = PlayerPrefs.GetFloat("hiscore", 0);
        if (slowdownCoroutine != null)
        {
            StopCoroutine(slowdownCoroutine);
            slowdownCoroutine = null;
        }

        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);
	
	

        if (isAlternateGame && !hasUsedExtraLife)
        {
            secondLifeButton.gameObject.SetActive(true);
        }

        UpdateHiscore(); // Check if a new high score was achieved
    }

    public void UpdateHiscore()
    {



       string hiscoreKey = isAlternateGame ? "hiscore_alternate" : "hiscore_original";
       float hiscore = PlayerPrefs.GetFloat(hiscoreKey, 0);


	if((score < hiscore) && (enabled == false)){
	    deathSound.Play();
	}
	

        // If the current score is higher than the saved high score
        if (score > hiscore)
        {
            PlayerPrefs.SetFloat(hiscoreKey, score); // Save the new high score
            PlayerPrefs.Save(); // Ensure it's saved to disk

            // Trigger high score sound and confetti only if it's a new high score
            if (highScoreSound != null)
            {
                highScoreSound.Play(); // Play high score sound
            }

            if (confettiManager != null)
            {
                confettiManager.PlayConfetti(); // Play confetti effect
            }
        }

        // Update the displayed high score for the current game mode
        hiscoreText.text = Mathf.FloorToInt(PlayerPrefs.GetFloat(hiscoreKey)).ToString("D5");
    }

    public void UseSecondLife()
    {
        hasUsedExtraLife = true;
        player.ResetPlayer();
        ClearObstacles();
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        secondLifeButton.gameObject.SetActive(false);
        gameSpeed = initialGameSpeed;
        spawner.gameObject.SetActive(true);
        enabled = true;
    }

    public void ClearObstacles()
    {
        Obstacle[] obstacles = FindObjectsOfType<Obstacle>();
        foreach (var obstacle in obstacles)
        {
            Destroy(obstacle.gameObject);
        }
    }

    public void SlowDown()
    {
        if (!isSlowedDown && slowdownCoroutine == null)
        {
            slowdownCoroutine = StartCoroutine(SlowDownCoroutine());
        }
    }

    private IEnumerator SlowDownCoroutine()
    {
        isSlowedDown = true;
        float originalSpeed = gameSpeed;
        gameSpeed = originalSpeed * (2f / 3f);
        player.SetJumpForce(player.defaultJumpForce * 1.2f);

        yield return new WaitForSecondsRealtime(5f);

        gameSpeed = originalSpeed;
        player.SetJumpForce(player.defaultJumpForce);
        isSlowedDown = false;
        slowdownCoroutine = null;
    }

    public void ResetHighScore()
    {
        string hiscoreKey = isAlternateGame ? "hiscore_alternate" : "hiscore_original";
        PlayerPrefs.SetFloat(hiscoreKey, 0);
        PlayerPrefs.Save(); // Ensure the change is saved
        InitializeHiscore(); // Update the displayed high score
    }

}
