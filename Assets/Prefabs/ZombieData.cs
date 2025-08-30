using UnityEngine;

[CreateAssetMenu(fileName = "ZombieData", menuName = "Scriptable Objects/ZombieData")]
public class ZombieData : ScriptableObject
{
    public float maxHealth;
    public float attackDistance;
    public float attackInterval;
    public int addScore;
}
