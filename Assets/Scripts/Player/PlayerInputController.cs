using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInputController : MonoBehaviour, GameInput.IGameplayActions
{
    private PlayerBase player;
    private GameInput input;

    public void OnAttack(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnInteract(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnJump(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnMove(InputAction.CallbackContext context)
    {
        switch (context.phase)
        {
            case InputActionPhase.Started:
                player.StartMove(context.ReadValue<Vector2>());
                break;
            case InputActionPhase.Performed:
                player.ProceedMove(context.ReadValue<Vector2>());
                break;
            case InputActionPhase.Canceled:
                player.StopMove();
                break;
        }
    }

    public void OnOpenInventory(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnPause(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    public void OnRotateCamera(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Performed)
        {
            var deltaAngles = context.ReadValue<Vector2>();
            deltaAngles.y *= -1;
            player.LookAngles += deltaAngles;
        }
    }

    public void OnSprint(InputAction.CallbackContext context)
    {
        throw new System.NotImplementedException();
    }

    private void Awake()
    {
        player = GetComponent<PlayerBase>();
        input = new GameInput();
        input.Gameplay.SetCallbacks(this);
    }

    private void OnEnable()
    {
        input.Enable();
    }

    private void OnDisable()
    {
        input.Disable();
    }
}
