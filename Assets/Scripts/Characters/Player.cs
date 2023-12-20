using System;
using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, Controls.IPlayerActions
{
    private Controls controls;

    ICharacter myCharacterType;

    private static int currentHealth;

    public bool isImmune = false;

    [SerializeField] private bool movementKeyHeld;

    public static int GetCurrentHealth()
    {
        return currentHealth;
    }

    private void Awake() => controls = new Controls();

    void Start()
    {
        gameObject.tag = "Player";

        myCharacterType = GetComponent<ICharacter>();

        currentHealth = myCharacterType.GetMaxHealth();
    }

    private void Update()
    {
        if (movementKeyHeld)
        {
            MovePlayer();
        }
    }

    private void OnEnable()
    {
        controls.Player.SetCallbacks(this);
        controls.Player.Enable();
    }

    private void OnDisable()
    {
        controls.Player.SetCallbacks(null);
        controls.Player.Disable();
    }

    void Controls.IPlayerActions.OnMoveset(InputAction.CallbackContext context)
    {
        if (context.started)
        {
            movementKeyHeld = true;
        }
        else if (context.canceled)
        {
            movementKeyHeld = false;
        }
    }

    void Controls.IPlayerActions.OnExit(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            Action.EscapeAction();
        }
    }

    private void MovePlayer()
    {
        Vector2 direction = controls.Player.Moveset.ReadValue<Vector2>();
        Vector2 roundedDirection = new Vector2(Mathf.Round(direction.x), Mathf.Round(direction.y));
        Vector3 futurePosition = transform.position + (Vector3)roundedDirection;

        if (IsValidPosition(futurePosition))
        {
            Action.MovementAction(GetComponent<Entity>(), direction, myCharacterType.GetMovementSpeed());
        }
    }

    private bool IsValidPosition(Vector3 futurePosition)
    {
        Vector3Int gridPosition = MapManager.instance.FloorMap.WorldToCell(futurePosition);

        if (!MapManager.instance.InBounds(gridPosition.x, gridPosition.y) || futurePosition == transform.position)
        {
            return false;
        }

        return true;
    }

    public void TakeDamage(int damage)
    {
        if (!isImmune)
        {
            currentHealth -= damage;

            Debug.Log("Player hp: " + currentHealth + "/" + myCharacterType.GetMaxHealth());

            StartCoroutine(ImmunityCoroutine());

            if (currentHealth <= 0)
            {
                Die();
            }
        }
    }

    private void Die()
    {
        Debug.Log("Player died");
    }

    public IEnumerator ImmunityCoroutine()
    {
        isImmune = true;

        yield return new WaitForSeconds(1.0f);

        isImmune = false;
    }
}
