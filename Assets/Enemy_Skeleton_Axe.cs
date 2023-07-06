using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeleton_Axe : Enemy
{
    #region States
    public SkeletonAxeIdleState idleState { get; private set; }
    public SkeletonAxeMoveState moveState { get; private set; }
    #endregion

    protected override void Awake()
    {
        base.Awake();
        idleState = new SkeletonAxeIdleState(this, stateMachine, "Idle", this);
        moveState = new SkeletonAxeMoveState(this, stateMachine, "Move", this);
    }

    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
    }
}
