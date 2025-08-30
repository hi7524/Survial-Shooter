using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    private int score;
    public bool IsPaused { get; private set; }


    private void Awake()
    {
        IsPaused = false;
    }

    private void Start()
    {
        //uiManager = GetComponent<UIManager>();
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
            Time.timeScale = 0;
            uiManager.SetActivePauseUI(true);
        }
        else
        {
            Time.timeScale = 1;
            uiManager.SetActivePauseUI(false);
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        uiManager.SetScoreText(score);
    }

    public void QuitGame()
    {
        Debug.Log("게임 종료");
        Application.Quit();
    }
}
