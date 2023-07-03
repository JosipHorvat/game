using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationTriggers : MonoBehaviour
{
    private Player player => GetComponentInParent<Player>();

    private void AnimationTrigger()
    {
        player.AnimationTrigger();
    }

    public void LastAttackFinished()
    {
        player.lastAttackFinished = true;
    }
    public void ExitFallBackAnimation()
    {
        player.lastAttackFinished = false;
    }
}
