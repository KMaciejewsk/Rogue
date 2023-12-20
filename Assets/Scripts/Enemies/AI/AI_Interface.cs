using UnityEngine;

public interface IAi
{
    public void FollowPlayer();

    public bool IsValidPosition(Vector3 futurePosition);

    public void DealDamageToPlayer(int damageDealt);

    public void TakeDamage(int damage);
}