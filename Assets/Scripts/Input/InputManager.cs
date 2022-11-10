using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, InputSystemPlayer.IPlayerActions
{
    public static InputManager Instance { get; private set; }
    public Vector2 Movement { get; private set;}

    private InputSystemPlayer mInputPlayer;

    private event EventHandler OnJumpHandler;
    private event EventHandler OnAttack1Handler;

    void Awake()
    {
        Instance = this;

        mInputPlayer = new InputSystemPlayer();
        mInputPlayer.Player.SetCallbacks(this);
    }

    void OnEnable()
    {
        mInputPlayer.Enable();
    }

    void OnDisable()
    {
        mInputPlayer.Disable();
    }

    public void OnMoverse(InputAction.CallbackContext context)
    {
        Movement = context.ReadValue<Vector2>();
    }

    public void OnSaltar(InputAction.CallbackContext context)
    {
        OnJumpHandler?.Invoke(this, EventArgs.Empty);
    }

    public void AddOnJumpHandler(EventHandler handler)
    {
        OnJumpHandler += handler;
    }

    public void AddOnAttackHandler(EventHandler handler)
    {
        OnAttack1Handler += handler;
    }

    public void OnFreeLookView(InputAction.CallbackContext context)
    {}

    public void OnAttack1(InputAction.CallbackContext context)
    {
        if (context.performed)
        {
            OnAttack1Handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
