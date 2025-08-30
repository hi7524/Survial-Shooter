using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;
    private PlayerHealth playerHealth;

    private int score;
    public bool IsPaused { get; private set; }
    public bool IsGameOver { get; private set; }

    private void Awake()
    {
        IsPaused = false;
        Time.timeScale = 1;
    }

    private void Start()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag(Tags.Player);
        PlayerHealth playerHealth = playerObj.GetComponent<PlayerHealth>();

        if (playerHealth != null)
        {
            playerHealth.OnDeath += EndGame;
        }
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Pause();
        }
    }

    public void Pause()
    {
        IsPaused = !IsPaused;

        if (IsPaused)
        {
            uiManager.SetActivePauseUI(true);
            if (!IsGameOver)
                Time.timeScale = 0;
        }
        else
        {
            uiManager.SetActivePauseUI(false);
            if (!IsGameOver)
                Time.timeScale = 1;
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        uiManager.SetScoreText(score);
    }

    public void EndGame()
    {
        Debug.Log("게임 오버 이펙트");
        uiManager.SetActiveGameOverUI(true);
        IsGameOver = true;
        Time.timeScale = 0;
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
