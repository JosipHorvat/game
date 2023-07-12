using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCounterAttackState : PlayerState
{
    public PlayerCounterAttackState(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.counterAttackDuration;
        player.anim.SetBool("SuccessfulCounterAttack", false);
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        player.SetZeroVelocity();

        Collider2D[] colliders = Physics2D.OverlapCircleAll(player.attackCheck.position, player.attackRadius);

        foreach (var hit in colliders)
        {
            if (hit.GetComponent<Enemy>() != null)
            {
               if(hit.GetComponent<Enemy>().CanBeStunned())
                {
                    stateTimer = 10; // this is needed longer so we dont exit state until we need to 
                    player.anim.SetBool("SuccessfulCounterAttack", true);
                }
            }     
        }
        if (stateTimer < 0 || triggerCalled)
            stateMachine.ChangeState(player.idleState);

    }


}