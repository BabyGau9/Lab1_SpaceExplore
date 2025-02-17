using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public GameObject backgroundGame1, backgroundGame2;
    public GameObject spaceShip1, spaceShip2;
    public GameObject enemySpawn1, enemySpawn2;
    public GameObject starSpawn;


    public GameObject playButton, exitButton, controlsButton, menuControls;
    public GameObject pauseButton, menuPause, scoreImage, scoreUITextGo, heartImage;
    public GameObject GameOverGo, backButton, againButton;
    public TextMeshProUGUI scoreOverGameText;

    private int currentScore = 0;
    private GameManagerState GMState;

    public enum GameManagerState
    {
        Opening, GamePlay, GamePause, GameOver
    }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject); // Ensures that GameManager persists across scenes
        }
        else
        {
            Destroy(gameObject); // Ensures there's only one instance
        }
    }
    private void UpdateGameManagerState()
    {
        switch (GMState)
        {
            case GameManagerState.Opening:
                SetOpeningUI();
                break;

            case GameManagerState.GamePlay:
                StartGamePlay();
                break;

            case GameManagerState.GameOver:
                HandleGameOver();
                break;
        }
    }
    private void SetOpeningUI()
    {
        playButton.SetActive(true);
        exitButton.SetActive(true);
        controlsButton.SetActive(true);
        againButton.SetActive(false);
        backButton.SetActive(false);
        GameOverGo.SetActive(false);
        scoreImage.SetActive(false);
        heartImage.SetActive(false);
        pauseButton.SetActive(false);
    }

    private void StartGamePlay()
    {
        currentScore = 0; // Reset score
        UpdateScoreUI();

        scoreImage.SetActive(true);
        heartImage.SetActive(true);
        pauseButton.SetActive(true);
        backgroundGame1.SetActive(true);
        backgroundGame2.SetActive(false);

        playButton.SetActive(false);
        exitButton.SetActive(false);
        controlsButton.SetActive(false);
        GameOverGo.SetActive(false);
        menuPause.SetActive(false);
        againButton.SetActive(false);
        backButton.SetActive(false);

        starSpawn.GetComponent<StarSpawner>().ScheduleStarSpawner();
        spaceShip1.GetComponent<SpaceShip1Controller>().Init();
        enemySpawn1.GetComponent<EnemySpawner>().ScheduleEnemySpawner();
    }
    private void HandleGameOver()
    {

        if (backgroundGame1.activeSelf)
        {
            enemySpawn1.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
        }
        else
        {
            enemySpawn2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
        }
        starSpawn.GetComponent<StarSpawner>().UnscheduleStarSpawner();

        GameOverGo.SetActive(true);
        againButton.SetActive(true);
        backButton.SetActive(true);

        exitButton.SetActive(false);
        playButton.SetActive(false);
        scoreImage.SetActive(false);
        heartImage.SetActive(false);
        pauseButton.SetActive(false);
        backgroundGame1.SetActive(false);

        // Show score on Game Over screen
        scoreOverGameText.text = "Score: " + currentScore.ToString();
    }
    public void SetGameManagerState(GameManagerState state)
    {
        GMState = state;
        UpdateGameManagerState();
    }

    public void StartGamePlayButton()
    {
        SetGameManagerState(GameManagerState.GamePlay);
    }

    public void AddScore(int points)
    {
        Debug.Log("Add " + points + " Point. Current score: " + currentScore);
        currentScore += points;
        UpdateScoreUI();

        if (currentScore >= 20 && backgroundGame1.activeSelf)
        {
            LoadLevel2();
        }
    }
    public void MinueScore(int points)
    {
        if(currentScore >= 5)
        {
            currentScore = currentScore - points;
            UpdateScoreUI();
        }
    }
    void UpdateScoreUI()
    {
        if (scoreUITextGo != null)
        {
            TextMeshProUGUI scoreText = scoreUITextGo.GetComponent<TextMeshProUGUI>();
            if (scoreText != null)
            {
                scoreText.text = currentScore.ToString("0000000");
            }
        }
    }

    public void LoadLevel2()
    {
        backgroundGame1.SetActive(false);
        spaceShip1.SetActive(false);
        enemySpawn1.SetActive(false);

        backgroundGame2.SetActive(true);
        spaceShip2.SetActive(true);
        enemySpawn2.SetActive(true);

        scoreImage.SetActive(true);
        heartImage.SetActive(true);
        pauseButton.SetActive(true);
        spaceShip2.GetComponent<SpaceShip2Controller>().Init();
        enemySpawn2.GetComponent<EnemySpawner2>().ScheduleEnemySpawner();

    }

    public void ShowControlsPanel()
    {
        menuControls.SetActive(true);
        playButton.SetActive(false);
        exitButton.SetActive(false);
        controlsButton.SetActive(false);
    }

    public void HideControlsPanel()
    {
        menuControls.SetActive(false);
        playButton.SetActive(true);
        exitButton.SetActive(true);
        controlsButton.SetActive(true);
    }

    // function of menu over game
    public void RestGame()
    {
        
        if (backgroundGame1.activeSelf)
        {
            enemySpawn1.SetActive(true);
        }
        else if (backgroundGame2.activeSelf)
        {
            enemySpawn2.SetActive(true);
        }
        SetGameManagerState(GameManagerState.GamePlay);
        UpdateGameManagerState();
    }

    public void BackToOpening()
    {
        SetGameManagerState(GameManagerState.Opening);
    }

    // function of button Pause
    public void PauseGame()
    {
        if (backgroundGame1.activeSelf)
        {
            enemySpawn1.GetComponent<EnemySpawner>().PauseSpawning();
        }
        else if (backgroundGame2.activeSelf)
        {
            enemySpawn2.GetComponent<EnemySpawner2>().PauseSpawning();
        }
        starSpawn.GetComponent<StarSpawner>().PauseSpawning();
        Time.timeScale = 0;
        pauseButton.SetActive(false);
        menuPause.SetActive(true);
    }

    public void ResumGame()
    {
        if (backgroundGame1.activeSelf)
        {
            enemySpawn1.GetComponent<EnemySpawner>().ResumeSpawning();
        }
        else
        {
            enemySpawn2.GetComponent<EnemySpawner2>().ResumeSpawning();
        }
        starSpawn.GetComponent<StarSpawner>().ResumeSpawning();
        Time.timeScale = 1;
        pauseButton.SetActive(true);
        menuPause.SetActive(false);
    }

    public void backToMenu()
    {
        currentScore = 0;
        if (backgroundGame1.activeSelf)
        {
            enemySpawn1.GetComponent<EnemySpawner>().UnscheduleEnemySpawner();
        }
        else
        {
            enemySpawn2.GetComponent<EnemySpawner2>().UnscheduleEnemySpawner();
        }
        spaceShip1.SetActive(true);
        spaceShip2.SetActive(true);
        enemySpawn1.SetActive(true);
        enemySpawn2.SetActive(true);

        backgroundGame1.SetActive(false);
        backgroundGame2.SetActive(false);
        menuPause.SetActive(false);
        Time.timeScale = 1;

        SetGameManagerState(GameManagerState.Opening);
    }
}
