using System;
using UnityEngine;

public class Player : Entity 
{
    #region States
    public PlayerStateMachine stateMachine { get; private set; }
    public PlayerIdleState idleState{ get; private set; }
    public PlayerMoveState moveState { get; private set; }
    public PlayerJumpState jumpState{ get; private set; }
    public PlayerAirState airState { get; private set; }
    public PlayerDashState dashState { get; private set; }
    #endregion
   

    [Header("Move Info")] 
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash Info")] 
    [SerializeField] private float dashCooldown;
    [SerializeField] private float dashUasgaeTimer; 
    public float dashDuration; 
    public float dashSpeed;
    public float dashDirection { get; private set; }
  

    [Header("Attack Info")] 
    [SerializeField] private float comboTimer; 
    private float _comboTimeWindow;
    private bool _isAttacking;
    private int _comboCounter;
    

    private void Awake()
    {
        stateMachine = new PlayerStateMachine();
        idleState = new PlayerIdleState(stateMachine,this,"idle");
        moveState = new PlayerMoveState(stateMachine, this, "move");
        jumpState = new PlayerJumpState(stateMachine,this,"jump");
        airState = new PlayerAirState(stateMachine, this, "jump");
        dashState = new PlayerDashState(stateMachine, this, "dash");
    }
    
 
    protected override void Start()
    {
        base.Start();
        stateMachine.Initialize(idleState);
    }

    // Update is called once per frame
    protected override void Update()
    {  
        base.Update();
        stateMachine.currentState.Update();
        CheckDashInput();
    }
    

    public void AttackOver()
    {
        _isAttacking = false;
        
        _comboCounter++;

        if (_comboCounter > 2)
        {
            _comboCounter = 0;
        }
        
    }
    

    private void CheckInput()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
    }

    private void CheckDashInput()
    {
        dashUasgaeTimer -= Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.LeftShift) && dashUasgaeTimer<0)
        {
            dashUasgaeTimer = dashCooldown;
            dashDirection = Input.GetAxisRaw("Horizontal");
            if (dashDirection == 0)
            {
                dashDirection = facingDirection;
            }
            stateMachine.ChangeState(dashState);
        }
    }

    private void StartAttackEvent()
    {
        if (isGroundDetected())
        {
            return;
        }
        if (_comboTimeWindow < 0)
        {
            _comboCounter = 0;
        }
            
        _isAttacking = true;
        _comboTimeWindow = comboTimer;
    }
    

    private void AnimatorControllers()
    {
        
        anim.SetBool("isAttacking",_isAttacking);
        anim.SetInteger("comboCounter", _comboCounter);
  
    }
    

    private void FlipController(float x)
    {
        //moving right and not facing right 
        if (x > 0 && !facingRight)
        {
            Flip();
        }
        //moving left and not facing left
        else if (x <0 && facingRight) 
        {
            Flip();
        }
    }
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
        FlipController(xVelocity);
    }
    
}
