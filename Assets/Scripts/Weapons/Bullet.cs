using UnityEngine;

public class Bullet : MonoBehaviour, IBullet
{
    private Vector2 bulletDirection;

    public float bulletSpeed = 0.4f;

    private int damageDealt = 2;

    public float GetBulletSpeed()
    {
        return bulletSpeed;
    }

    public int GetDamageDealt()
    {
        return damageDealt;
    }

    public void SetDirection(Vector2 direction)
    {
        bulletDirection = direction;
    }

    void Update()
    {
        MoveBullet();

        DealDamage();
    }

    private void MoveBullet()
    {
        Vector3 futurePosition = transform.position + (Vector3)bulletDirection;
        if (IsValidPosition(futurePosition))
        {
            Action.MovementAction(GetComponent<Entity>(), bulletDirection, bulletSpeed);
        }
        else
        {
            DestroyBullet();
        }
    }

    private bool IsValidPosition(Vector3 futurePosition)
    {
        Vector3Int gridPosition = MapManager.instance.FloorMap.WorldToCell(futurePosition);

        if (MapManager.instance.ObstacleMap.HasTile(gridPosition) || futurePosition == transform.position)
        {
            return false;
        }

        return true;
    }

    private void DestroyBullet()
    {
        Destroy(gameObject);
    }

    public void DealDamage()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy"))
            {
                IAi aiScript = collider.GetComponent<IAi>();
                aiScript.TakeDamage(damageDealt);

                DestroyBullet();
            }
        }
    }
}
