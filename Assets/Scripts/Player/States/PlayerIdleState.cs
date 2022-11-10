using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerIdleState : State<PlayerController>
{
    private Animator mAnimator;

    public PlayerIdleState(
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
        if (InputManager.Instance.Movement != Vector2.zero)
        {
            mAnimator.SetFloat("Speed", 1f, mController.dampTime, Time.deltaTime);
            // Cambiar a un estado RunningState
            mFsm.ChangeState(mController.PlayerRunningState);
        }else
        {
            mAnimator.SetFloat("Speed", 0f, mController.dampTime, Time.deltaTime);
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
        mAnimator = mController.GetComponent<Animator>();

        mAnimator.Play("Blend Tree");
    }

    private void Attack1(object sender, EventArgs e)
    {
        // Delegado que se ejecutara cuando se haga click izquierdo
        // con el mouse
        mFsm.ChangeState(mController.PlayerAttackingState); // Cambiar a estado de Ataque
    }

    public override void OnStop()
    {
        base.OnStop();
    }
}
