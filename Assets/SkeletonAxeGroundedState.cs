using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAxeGroundedState : EnemyState
{
    protected Enemy_Skeleton_Axe enemy;
    public SkeletonAxeGroundedState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton_Axe _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Exit()
    {
        base.Exit();
    }

    public override void Update()
    {
        base.Update();

        if (enemy.IsPlayerDetected())
            stateMachine.ChangeState(enemy.battleState);
    }
}
