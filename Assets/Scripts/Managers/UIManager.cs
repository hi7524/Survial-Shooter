using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [Space]
    [SerializeField] private GameObject effectUi;
    [SerializeField] private GameObject pauseUi;
    [SerializeField] private GameObject gameOverUi;
    [Space]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI gameOverText;

    private Animator scoreAnimator;


    private void Start()
    {
        SetActivePauseUI(false);
        SetActiveGameOverUI(false);

        scoreAnimator = scoreText.transform.GetComponent<Animator>();
    }

    public void SetHealthBarValue(float amount)
    {
        healthBar.value = amount;
    }

    public void SetActivePauseUI(bool active)
    {
        pauseUi.SetActive(active);
    }

    public void SetScoreText(int score)
    {
        scoreAnimator.SetTrigger("Add");
        scoreText.text = $"SCORE: {score:N0}";
    }

    public void PlayDamagedEffect()
    {
        StartCoroutine(DamagedEffect());
    }

    private IEnumerator DamagedEffect()
    {
        effectUi.SetActive(true);
        yield return new WaitForSeconds(0.2f);
        effectUi.SetActive(false);
    }

    public void SetActiveGameOverUI(bool active)
    {
        gameOverUi.SetActive(active);
    }
}