using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D leftUpper;
    public Rigidbody2D leftLower;
    public Rigidbody2D rightLower;
    public Rigidbody2D rightUpper;
    public Rigidbody2D tail;

    public float limbMovementSpeed;
    public float limbMaxSpeed;

    public bool leftUpperActive;
    public bool leftLowerActive;
    public bool rightLowerActive;
    public bool rightUpperActive;
    public bool tailActive;

    //Event Subscriptions
    void OnEnable()
    {
        InputManager.onLeftUpper += ToggleLeftUpper;
        InputManager.onLeftLower += ToggleLeftLower;
        InputManager.onRightUpper += ToggleRightUpper;
        InputManager.onRightLower += ToggleRightLower;
        InputManager.onTail += ToggleTail;
        InputManager.OnLookUpdated += MoveLimbs;
    }

    void OnDisable()
    {
        InputManager.onLeftUpper -= ToggleLeftUpper;
        InputManager.onLeftLower -= ToggleLeftLower;
        InputManager.onRightUpper -= ToggleRightUpper;
        InputManager.onRightLower -= ToggleRightLower;
        InputManager.onTail -= ToggleTail;
        InputManager.OnLookUpdated -= MoveLimbs;
    }

    private void ToggleLeftUpper(bool toggle)
    {
        leftUpperActive = toggle;
    }
    private void ToggleLeftLower(bool toggle)
    {
        leftLowerActive = toggle;
    }
    private void ToggleRightLower(bool toggle)
    {
        rightLowerActive = toggle;
    }
    private void ToggleRightUpper(bool toggle)
    {
        rightUpperActive = toggle;
    }
    private void ToggleTail(bool toggle)
    {
        tailActive = toggle;
    }

    private void MoveLimbs(Vector2 movementDelta)
    {
        
        float limbSpeed = Mathf.Min(limbMovementSpeed * movementDelta.magnitude, limbMaxSpeed);
        Vector2 limbMovement = limbSpeed * Time.deltaTime * movementDelta.normalized;

        if (leftUpperActive)
        {
            leftUpper.MovePosition(leftUpper.transform.position + (Vector3)limbMovement);
        }

        if (leftLowerActive)
        {
            leftLower.MovePosition(leftLower.transform.position + (Vector3)limbMovement);
        }

        if (rightLowerActive)
        {
            rightLower.MovePosition(rightLower.transform.position + (Vector3)limbMovement);
        }

        if (rightUpperActive)
        {
            rightUpper.MovePosition(rightUpper.transform.position + (Vector3)limbMovement);
        }

        if (tailActive)
        {
            tail.MovePosition(tail.transform.position + (Vector3)limbMovement);
        }
    }
}
