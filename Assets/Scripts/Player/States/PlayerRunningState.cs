using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRunningState : State<PlayerController>
{
    private CharacterController mCharacterController;
    private Animator mAnimator;

    public PlayerRunningState(
        PlayerController controller, 
        FiniteStateMachine<PlayerController> fsm) : base(controller, fsm)
    {}

    public override void HandleInput()
    {
        base.HandleInput();
    }

    public override void OnLogicUpdate()
    {
        base.OnLogicUpdate();

        Vector3 movementVector = CalculateDirection(InputManager.Instance.Movement);

        mCharacterController.Move(
            movementVector * Time.deltaTime * mController.speed
        );

        if (InputManager.Instance.Movement == Vector2.zero)
        {
            mAnimator.SetFloat("Speed", 0f, mController.dampTime, Time.deltaTime);
            // Regresar al Idle State
            mFsm.ChangeState(mController.PlayerIdleState);
        }
        else
        {
            mAnimator.SetFloat("Speed", 1f, mController.dampTime, Time.deltaTime);
            // Rotation
            mController.transform.rotation = Quaternion.Lerp(
                mController.transform.rotation,
                Quaternion.LookRotation(movementVector),
                mController.rotationDampTime
            );
        }
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
    }

    public override void OnStart()
    {
        base.OnStart();

        InputManager.Instance.AddOnAttackHandler(Attack1);

        mCharacterController = mController.GetComponent<CharacterController>();
        mAnimator = mController.GetComponent<Animator>();
    }

    private void Attack1(object sender, EventArgs e)
    {
        mFsm.ChangeState(mController.PlayerAttackingState); // Cambiar al estado de Ataque
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    private Vector3 CalculateDirection(Vector2 mov)
    {
        var dirForward = mController.MainCamera.transform.forward;
        var dirRight = mController.MainCamera.transform.right;

        dirForward.y = 0f;
        dirRight.y = 0f;

        dirForward.Normalize();
        dirRight.Normalize();

        return dirForward * mov.y + dirRight * mov.x;
    }
}
