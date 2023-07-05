using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Animator anim { get; private set; }

    public EnemyStateMachine stateMachine { get; private set; }

    private void Awake()
    {
        stateMachine = new EnemyStateMachine();
    }

    private void Update()
    {
        stateMachine.currentState.Update();
    }
}
