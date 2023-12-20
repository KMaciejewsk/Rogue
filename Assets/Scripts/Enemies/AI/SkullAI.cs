using UnityEngine;

public class SkullAI : MonoBehaviour, IAi
{
    private Transform target;

    IEnemy myEnemyType;

    private int currentHealth;

    void Start()
    {
        target = GameObject.FindWithTag("Player").transform;

        myEnemyType = GetComponent<IEnemy>();

        currentHealth = myEnemyType.GetMaxHealth();
    }

    void Update()
    {
        FollowPlayer();

        DealDamageToPlayer(myEnemyType.GetDamageDealt());
    }

    public void FollowPlayer()
    {
        Vector2 directionToPlayer = target.position - transform.position;
        Vector3 futurePosition = transform.position + (Vector3)directionToPlayer * myEnemyType.GetMovementSpeed();

        if (IsValidPosition(futurePosition))
        {
            Action.MovementAction(GetComponent<Entity>(), directionToPlayer, myEnemyType.GetMovementSpeed());
        }
    }

    public bool IsValidPosition(Vector3 futurePosition)
    {
        // Check if the future position is valid
        Vector3Int gridPosition = MapManager.instance.FloorMap.WorldToCell(futurePosition);

        if (MapManager.instance.ObstacleMap.HasTile(gridPosition) || futurePosition == transform.position)
        {
            return false;
        }

        // Check if there is another enemy in the future position
        Collider2D[] colliders = Physics2D.OverlapCircleAll(futurePosition, 1);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Enemy") && collider.transform != transform)
            {
                return false;
            }
        }

        return true;
    }

    public void DealDamageToPlayer(int damageDealt)
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1);

        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Player"))
            {
                Player playerScript = collider.GetComponent<Player>();
                playerScript.TakeDamage(myEnemyType.GetDamageDealt());

                DestroyEnemy();
            }
        }
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;

        if (currentHealth <= 0)
        {
            DestroyEnemy();
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject);
    }
}
