using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar;
    [SerializeField] private GameObject effectUi;


    public void SetHealthBarValue(float amount)
    {
        healthBar.value = amount;
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