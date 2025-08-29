using UnityEngine;

public class GameManager : MonoBehaviour
{
    private int score;


    public void AddScore(int amount)
    {
        score += amount;
    }
}
