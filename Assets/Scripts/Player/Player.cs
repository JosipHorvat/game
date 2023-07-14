using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Entity
{
    public CapsuleCollider2D playerCapsuleCollider { get; private set; }

    [Header("Attack details")]
    public Vector2[] attackMovement;
    public float counterAttackDuration = .2f;

    public bool isBusy { get; private set; }

    [Header("Move info")]
    public float moveSpeed = 12;
    public float jumpForce;

    [Header("Dash info")]
    public float dashSpeed;
    public float dashDuration;
    public float dashDir { get; private set; }

    public SkillManager skill { get; private set; }

    [Header("Crouch info")]
    public float crouchColliderHeight = 1.4f;
    public float standColliderHeight = 2.26f;
    public float crouchMoveSpeed = 5; 
    private Vector2 crouchWorkSpace;

    public bool lastAttackFinished { get; set; }

    #region States
    public PlayerStateMachine stateMachine { get; private set; }
  
    public PlayerIdleState idleState { get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState { get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    public PlayerWallSlideState wallSlide { get; private set; }
    public PlayerWallJumpState wallJump { get; private set; }

    public PlayerPrimaryAttackState primaryAttack { get; private set; }
    public PlayerFallBackState fallBackState { get; private set; }

    public PlayerCrouchIdleState crouchIdleState { get; private set; }
    public PlayerCrouchMoveState crouchMoveState { get; private set; }
    public PlayerCounterAttackState counterAttack { get; private set; }

    public PlayerThrownWeaponAimState aimState { get; private set; }
    public PlayerThrownWeaponCatchState catchState { get; private set; }
    #endregion

    #region Unity CallBack Functions
    protected override void Awake()
    {
        base.Awake();
        stateMachine = new PlayerStateMachine();

        idleState = new PlayerIdleState(this, stateMachine, "Idle");
        moveState = new PlayerMoveState(this, stateMachine, "Move");
        jumpState = new PlayerJumpState(this, stateMachine, "Jump");
        airState  = new PlayerAirState(this, stateMachine, "Jump");
        dashState = new PlayerDashState(this, stateMachine, "Dash");
        wallSlide = new PlayerWallSlideState(this, stateMachine, "WallSlide");
        wallJump = new PlayerWallJumpState(this, stateMachine, "Jump");

        primaryAttack = new PlayerPrimaryAttackState(this, stateMachine, "Attack");
        fallBackState = new PlayerFallBackState(this, stateMachine, "FallBack");

        crouchIdleState = new PlayerCrouchIdleState(this, stateMachine, "CrouchIdle");
        crouchMoveState = new PlayerCrouchMoveState(this, stateMachine, "CrouchMove");
        counterAttack = new PlayerCounterAttackState(this, stateMachine, "CounterAttack");

        aimState = new PlayerThrownWeaponAimState(this, stateMachine, "AimWithThrown");
        catchState = new PlayerThrownWeaponCatchState(this, stateMachine, "CatchThrown");
    }

    protected override void Start()
    {
        base.Start();

        skill = SkillManager.instance;

        playerCapsuleCollider = GetComponent<CapsuleCollider2D>();

        stateMachine.Initialize(idleState);
    }

    protected override void Update()
    {
        base.Update();
        stateMachine.currentState.Update();
     
        CheckForDashInput();
    }
    #endregion

    public IEnumerator BusyFor(float _seconds)
    {
        isBusy = true;

        yield return new WaitForSeconds(_seconds);
     
        isBusy = false;
    }

    public void AnimationTrigger() => stateMachine.currentState.AnimationFinishTrigger();

    private void CheckForDashInput()
    {
        if (IsWallDetected())
            return;

        if (Input.GetKeyDown(KeyCode.LeftShift) && SkillManager.instance.dash.CanUseSkill())
        {
           
            dashDir = Input.GetAxisRaw("Horizontal");

            if (dashDir == 0)
                dashDir = facingDir;

            stateMachine.ChangeState(this.dashState);
        }          
    }

    #region Collision 
    public void SetColliderHeight(float _height)
    {
        Vector2 center = playerCapsuleCollider.offset;
        crouchWorkSpace.Set(playerCapsuleCollider.size.x, _height);

        center.y += (_height - playerCapsuleCollider.size.y) / 2;

        playerCapsuleCollider.size = crouchWorkSpace;
        playerCapsuleCollider.offset = center;

    }
    public void SetWallCheckPositionY(float _height, bool lowerPosition)
    {
        Vector3 tempWallCheck = wallCheck.position;

        if (lowerPosition)
            tempWallCheck.y -= _height;
        else
            tempWallCheck.y += _height;

        wallCheck.position = tempWallCheck;
    }
    #endregion

}
