using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAxeBattleState : EnemyState
{
    private Enemy_Skeleton_Axe enemy;
    private Transform player;
    private int moveDir;

    public SkeletonAxeBattleState(Enemy _enemyBase, EnemyStateMachine _stateMachine, string _animBoolName, Enemy_Skeleton_Axe _enemy) : base(_enemyBase, _stateMachine, _animBoolName)
    {
        this.enemy = _enemy;
    }

    public override void Enter()
    {
        base.Enter();

      player = GameObject.Find("Player").transform;  // I don't have Player manager, instead I am declaring player this way.
    }


    public override void Update()
    {
        base.Update();

        if(enemy.IsPlayerDetected())
        {
            if(enemy.IsPlayerDetected().distance < enemy.attackDistance)
            {
                Debug.Log("Attack!!!");
                enemy.ZeroVelocity();
                return;
            }
        }

        // If it is bigger, then player is on the right side of enemy and enemy moves right
        if (player.position.x > enemy.transform.position.x) 
            moveDir = 1;
        else if (player.position.x < enemy.transform.position.x)
            moveDir = -1;

        enemy.SetVelocity(enemy.moveSpeed * moveDir, rb.velocity.y);
    }

    public override void Exit()
    {
        base.Exit();
    }

}
