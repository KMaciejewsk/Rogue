using UnityEngine;

public class Skull : MonoBehaviour, IEnemy
{
    private int maxHealth = 6;

    private float movementSpeed = 0.01f;

    private int damageDealt = 1;

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }

    public int GetDamageDealt()
    {
        return damageDealt;
    }
}
