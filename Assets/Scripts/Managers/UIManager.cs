using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Slider healthBar; 


    public void SetHealthBarValue(float amount)
    {
        healthBar.value = amount;
    }
}
