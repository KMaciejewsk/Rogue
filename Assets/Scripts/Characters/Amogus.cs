using UnityEngine;

public class Amogus : MonoBehaviour, ICharacter
{
    private int maxHealth = 10;

    private float movementSpeed = 0.1f;

    public float GetMovementSpeed()
    {
        return movementSpeed;
    }

    public int GetMaxHealth()
    {
        return maxHealth;
    }
}