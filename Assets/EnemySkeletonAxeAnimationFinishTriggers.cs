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
}
