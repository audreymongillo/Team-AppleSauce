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
    public ConfettiManager confettiManager = null;

    private Player player;
    private Spawner spawner;
    public float score;
    public static float currentScore;

    private const string FirstRunKey = "FirstRun";
    private bool isSlowedDown = false;
    private Coroutine slowdownCoroutine = null;
    private bool hasUsedExtraLife = false;



    public AudioSource deathSound;

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

        if (PlayerPrefs.GetInt(FirstRunKey, 0) == 0)
        {
            PlayerPrefs.SetFloat("hiscore", 0);
            PlayerPrefs.SetInt(FirstRunKey, 1);
            PlayerPrefs.Save();
        }

        NewGame();
    }

    public void NewGame()
    {
        if (slowdownCoroutine != null)
        {
            StopCoroutine(slowdownCoroutine);
            slowdownCoroutine = null;
        }
        isSlowedDown = false;

        hasUsedExtraLife = false;

        ClearObstacles();

        score = 0f;
        gameSpeed = initialGameSpeed;

        enabled = true;
        player.gameObject.SetActive(true);
        spawner.gameObject.SetActive(true); // Ensure the spawner is active
        gameOverText.gameObject.SetActive(false);
        retryButton.gameObject.SetActive(false);
        secondLifeButton.gameObject.SetActive(false);
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
    }

    public void GameOver()

    {

	    float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        // Stop the slowdown coroutine and reset speed to prevent continued movement
        if (!hasUsedExtraLife)
        {
            secondLifeButton.gameObject.SetActive(true);
        }
        else
        {
            secondLifeButton.gameObject.SetActive(false);
        }

        if (slowdownCoroutine != null)
        {
            StopCoroutine(slowdownCoroutine);
            slowdownCoroutine = null;
        }

	if(score < hiscore)
	{

		deathSound.Play();
	}

        gameSpeed = 0f;
        enabled = false;
        player.gameObject.SetActive(false);
        spawner.gameObject.SetActive(false); // Disable spawner to stop generating obstacles
        gameOverText.gameObject.SetActive(true);
        retryButton.gameObject.SetActive(true);

        UpdateHiscore();
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
        
        spawner.gameObject.SetActive(true); // Activate the spawner to start generating obstacles again
        enabled = true; // Enable GameManager updates
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

    public void UpdateHiscore()
    {
        float hiscore = PlayerPrefs.GetFloat("hiscore", 0);

        if (score > hiscore)
        {
            hiscore = score;
            PlayerPrefs.SetFloat("hiscore", hiscore);
            PlayerPrefs.Save();

            if (confettiManager != null)
            {
                confettiManager.PlayConfetti();
            }
        }

        hiscoreText.text = Mathf.FloorToInt(hiscore).ToString("D5");
    }
}
