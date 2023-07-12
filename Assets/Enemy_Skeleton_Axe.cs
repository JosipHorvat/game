using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton_Axe : Enemy
{
    #region States
    public SkeletonAxeIdleState idleState { get; private set; }
    public SkeletonAxeMoveState moveState { get; private set; }
    public SkeletonAxeBattleState battleState { get; private set; }
    public SkeletoAxeAttackState attackState { get; private set; }
    public SkeletonStunnedState stunnedState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonAxeIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonAxeMoveState(this, stateMachine, "Move", this);
        battleState = new SkeletonAxeBattleState(this, stateMachine, "Move", this);
        attackState = new SkeletoAxeAttackState(this, stateMachine, "Attack", this);
        stunnedState = new SkeletonStunnedState(this, stateMachine, "Stunned", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        if (Input.GetKeyDown(KeyCode.U))
            stateMachine.ChangeState(stunnedState);
    }

    public override bool CanBeStunned()
    {
       if(base.CanBeStunned())
        {
            stateMachine.ChangeState(stunnedState);
            return true;
        }
        return false;
    }
}
