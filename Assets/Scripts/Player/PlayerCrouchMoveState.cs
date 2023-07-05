using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchMoveState : PlayerGroundedState
{
    public PlayerCrouchMoveState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.SetColliderHeight(player.crouchColliderHeight);
        player.SetWallCheckPositionY(1f, true);
    }

    public override void Exit()
    {
        base.Exit();
        player.SetColliderHeight(player.standColliderHeight);
        player.SetWallCheckPositionY(1f, false);
    }


    public override void Update()
    {
        player.SetVelocity(xInput * player.crouchMoveSpeed, rb.velocity.y);

        base.Update();
        if (xInput == 0 || player.IsWallDetected())
            stateMachine.ChangeState(player.crouchIdleState);
        else if (yInput != -1 && !player.IsCeilingDetected)
            stateMachine.ChangeState(player.moveState);
    }
}
