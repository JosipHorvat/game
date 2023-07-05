using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCrouchIdleState : PlayerGroundedState
{
    public PlayerCrouchIdleState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        player.ZeroVelocity();
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
        base.Update();
        if (xInput == player.facingDir && player.IsWallDetected())
            return;

        if (xInput != 0)
            stateMachine.ChangeState(player.crouchMoveState);
        else if(yInput != -1 && !player.IsCeilingDetected)
            stateMachine.ChangeState(player.idleState);
    }
}
