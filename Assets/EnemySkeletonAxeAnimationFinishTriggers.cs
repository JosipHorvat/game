using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySkeletonAxeAnimationFinishTriggers : MonoBehaviour
{
    private Enemy_Skeleton_Axe enemy => GetComponentInParent<Enemy_Skeleton_Axe>();

    private void AnimationTrigger()
    {
        enemy.AnimationFinishTrigger();
    }

    private void AttackTrigger()
    {
        Collider2D[] colliders = Physics2D.OverlapCircleAll(enemy.attackCheck.position, enemy.attackRadius);

        foreach(var hit in colliders)
        {
            if (hit.GetComponent<Player>() != null)
                hit.GetComponent<Player>().Damage();
        }
    }
}
