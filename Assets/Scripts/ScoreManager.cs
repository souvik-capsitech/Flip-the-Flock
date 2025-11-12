using UnityEngine;
using TMPro;
using UnityEngine.UI;



public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI mistakeText;
    public TextMeshProUGUI bestScoreText;
    public GameObject gameOverPanel;
    public int maxMistakes = 6;
    public TextMeshProUGUI gameOverScoreText;

    private int score = 0;
    private int mistakes = 0;
    private bool gameOver = false;

    private int bestScore = 0;
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        LoadBestScore();
        ResetScore();   
    }

    public void AddScore(int points =1)
    {
        if (gameOver) return;

        score += points;
        UpdateScoreUI();

        if(score >bestScore)
        {
            bestScore = score;
            SaveBestScore();
            UpdateBestScoreUI();
        }

    }

    public void ResetScore()
    {
        score = 0;
        mistakes = 0;
        gameOver = false;
        Time.timeScale = 1f;

        UpdateScoreUI();
        UpdateMistakeUI();
        UpdateBestScoreUI();

        if (gameOverPanel != null)
            gameOverPanel.SetActive(false);

    }


    public void AddMistake()
    {
        if(gameOver) return;

        mistakes++;
        UpdateMistakeUI();

        if(mistakes >= maxMistakes)
        {
            EndGame();
        }
    }


    void UpdateScoreUI()
    {
        if(scoreText != null)
        {
            scoreText.text = "" + score;
        }

    }

    void UpdateBestScoreUI()
    {
        if (bestScoreText != null)
            bestScoreText.text = "Best Score:  " + bestScore;
    }
    void UpdateMistakeUI()
    {
        if (mistakeText != null)
            mistakeText.text = mistakes + "/" + maxMistakes;
    }


    public bool IsGameOver()
    {
        return gameOver;
    }

    void EndGame()
    {
        gameOver = true;

        if (GameManager.instance != null)
            GameManager.instance.StopAllCoroutines();

        Time.timeScale = 0f;

        if (gameOverPanel != null)
        {
            gameOverPanel.SetActive(true);
            AudioManager.instance.PlaySFX(AudioManager.instance.loseSFX);


            if (gameOverScoreText != null)
                gameOverScoreText.text = "" + score;

            Debug.Log("Game Over Panel Activated");

        
            //RectTransform panel = gameOverPanel.GetComponent<RectTransform>();

          
            //panel.anchoredPosition = new Vector2(0, 157);

    
            //LeanTween.moveY(panel, 0, 0.6f)
            //    .setEaseOutBounce();
        }
        else
        {
            Debug.LogError("GameOverPanel is not assigned in ScoreManager!");
        }
    }


    void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        PlayerPrefs.Save();
    }
    void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
    }

    // Update is called once per frame
    public int GetMistakeCount() => mistakes;
    public int GetScore() => score;
    public int GetBestScore() => bestScore;

    void Update()
    {
        
    }
}
