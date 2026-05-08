using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    [Header("UI")]
    public TextMeshProUGUI scoreText;
    public GameObject gameOverPanel;
    public GameObject gameClearPanel;

    private DialogueManager dialogueManager;
    private int score = 0;
    private int totalSushi;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    void Start()
    {
        totalSushi = GameObject.FindGameObjectsWithTag("Sushi").Length;
        dialogueManager =  FindAnyObjectByType<DialogueManager>();
        gameOverPanel.SetActive(false);
        gameClearPanel.SetActive(false);
        score = 0;
        UpdateScoreUI();
    }

    public void SushiCollected()
    {
        score++;
        UpdateScoreUI();

        int remaining = GameObject.FindGameObjectsWithTag("Sushi").Length;

        if (remaining <= 0)
        {
            // 마지막 초밥 → 클리어 대사만
            dialogueManager.PlayGameClear();
            GameClear();
        }
        else
        {
            // 일반 초밥 대사
            dialogueManager.PlayEatSushi();
        }
    }

    public void GameOver()
    {
        gameOverPanel.SetActive(true);
        Time.timeScale = 0f;
        dialogueManager.PlayGameOver();
    }

    public void GameClear()
    {
        gameClearPanel.SetActive(true);
        Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        score = 0;
        UnityEngine.SceneManagement.SceneManager.LoadScene(
            UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
        );
    }

    void UpdateScoreUI()
    {
        scoreText.text = "Score " + score * 100;
    }
}