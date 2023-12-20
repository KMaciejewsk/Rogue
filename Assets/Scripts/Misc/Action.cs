using UnityEngine;

static public class Action
{
    static public void EscapeAction()
    {
        Debug.Log("Quit");
    }

    static public void MovementAction(Entity entity, Vector2 direction, float movementSpeed)
    {
        //Debug.Log($"{entity.name} moves {direction}"})
        entity.Move(direction, movementSpeed);
    }
}