using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;

    [SerializeField] private GameObject effectUi;
    [SerializeField] private GameObject pauseUi;

    [SerializeField] private TextMeshProUGUI scoreText;


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
        scoreText.text = $"SCORE: {score:N0}";
    }

    public void PlayDamagedEffect()
    {
        StartCoroutine(DamagedEffect());
    }

    private IEnumerator DamagedEffect()
    {
        effectUi.SetActive(true);
        yield return new WaitForSeconds(0.15f);
        effectUi.SetActive(false);
    }
}