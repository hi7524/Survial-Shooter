using UnityEngine;

public class GameManager : MonoBehaviour
{
    public UIManager uiManager;

    private int score;
    private bool pause;


    private void Awake()
    {
        pause = false;
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

    private void Pause()
    {
        pause = !pause;

        if (pause)
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
    }
}
