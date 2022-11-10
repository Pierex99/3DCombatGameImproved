using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackingState : State<PlayerController>
{
    private Animator mAnimator;

    public PlayerAttackingState(
        PlayerController controller,
        FiniteStateMachine<PlayerController> fsm
    ) : base(controller, fsm)
    {
    }

    public override void OnStart()
    {
        base.OnStart();
        mAnimator = mController.GetComponent<Animator>();
        mAnimator.Play("Attacking1");
    }

    public override void OnStop()
    {
        base.OnStop();
    }

    public override void OnLogicUpdate()
    {
        base.OnLogicUpdate();
        if (mController.OnAttackingAnimationEnd)
        {
            mController.OnAttackingAnimationEnd = false;
            mFsm.ChangeState(mController.PlayerIdleState);
        }
    }

    public override void OnPhysicsUpdate()
    {
        base.OnPhysicsUpdate();
    }
}
