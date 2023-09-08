using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : Singleton<InputManager>
{
    private PlayerInput playerInput;
    [SerializeField] private Vector3 lookDelta;

    public static event Action OnPrimaryPressed;
    public static event Action OnSecondaryPressed;
    public static event Action<Vector2> OnLookUpdated;

    //Left Upper
    public bool leftUpper;
    public static event Action<bool> onLeftUpper;
    //Left Lower
    public bool leftLower;
    public static event Action<bool> onLeftLower;
    //Right Lower
    public bool rightLower;
    public static event Action<bool> onRightLower;
    //Right Upper
    public bool rightUpper;
    public static event Action<bool> onRightUpper;
    //Tail
    public bool tail;
    public static event Action<bool> onTail;


    void Awake()
    {
        if(playerInput == null) playerInput = GetComponent<PlayerInput>();
    }
    // Start is called before the first frame update
    void Start()
    {
        if (!playerInput.camera)
            playerInput.camera = Camera.main;

    }

    public void OnLook(InputAction.CallbackContext context)
    {
        if (!context.performed) return;
        Vector2 pos = context.ReadValue<Vector2>();
        lookDelta = pos;
        OnLookUpdated?.Invoke(lookDelta);
    }

    public void OnPrimary(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnPrimaryPressed?.Invoke();
        }
        else if (context.canceled)
        {

        }
            
    }public void OnSecondary(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnSecondaryPressed?.Invoke();
        }
        else if (context.canceled)
        {

        }
            
    }

    public void OnLeftUpper(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leftUpper = true;
        }
        else if (context.canceled)
        {
            leftUpper = false;
        }
        onLeftUpper?.Invoke(leftUpper);
    }

    public void OnLeftLower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            leftLower = true;
        }   
        else if (context.canceled)
        {
            leftLower = false;
        }
        onLeftLower?.Invoke(leftLower);

    }

    public void OnRightUpper(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rightUpper = true;
        }
        else if (context.canceled)
        {
            rightUpper = false;
        }
        onRightUpper?.Invoke(rightUpper);

    }

    public void OnRightLower(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            rightLower = true;
        }
        else if (context.canceled)
        {
            rightLower = false;
        }
        onRightLower?.Invoke(rightLower);

    }

    public void OnTail(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            tail = true;
        }
        else if (context.canceled)
        {
            tail = false;
        }
        onTail?.Invoke(tail);

    }




}
