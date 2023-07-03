using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPrimaryAttack : PlayerState
{
    private int comboCounter;

    private float lastTimeAttacked;
    private float comboTimeWindow = .5f;


    public PlayerPrimaryAttack(Player _player, PlayerStateMachine _stateMachine, string _animBoolName) : base(_player, _stateMachine, _animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
        if (comboCounter > 2 || Time.time >= lastTimeAttacked + comboTimeWindow)
        {
            comboCounter = 0;
        
        }
        player.anim.SetInteger("ComboCounter", comboCounter);
    }

    public override void Exit()
    {
        base.Exit();

        comboCounter++;
        lastTimeAttacked = Time.time;
    }

    public override void Update()
    {
        base.Update();

        if (stateTimer < 0)
            rb.velocity = new Vector2(0, 0);

        if (player.lastAttackFinished == true)
        {
            stateMachine.ChangeState(player.fallBackState);
            return;
        }
           
        if (triggerCalled)
            stateMachine.ChangeState(player.idleState);
        
    }
}