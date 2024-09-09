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
    #endregion
   

    [Header("Move Info")] 
    public float moveSpeed;
    public float jumpForce;

    [Header("Dash Info")] 
    [SerializeField] private float dashDuration; 
    [SerializeField] private float dashSpeed;
    [SerializeField] private float _dashTime;
    
    [SerializeField] private float dashCooldown;
    [SerializeField]private float _dashCooldownTime;

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
        
        CheckInput();
        
        _dashTime -= Time.deltaTime; //Time.deltaTime: time from last frame 
        _dashCooldownTime -= Time.deltaTime;
        
        
        FlipController();
        AnimatorControllers();

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
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            DashAbility();
        }
    }

    private void StartAttackEvent()
    {
        if (!_isGrounded)
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

    private void DashAbility()
    {
        if (_dashCooldownTime < 0)
        {
            _dashCooldownTime = dashCooldown;
            _dashTime = dashDuration;
        }
    }
    
    private void Movement()
    {
        if (_isAttacking)
        {
            rb.velocity = new Vector2(0,0);
        }
        else if (_dashTime > 0)
        {
            rb.velocity = new Vector2(_facingDirection * dashSpeed,0);
        }
        
    }
    

    private void AnimatorControllers()
    {
        bool isDashing = _dashTime > 0;
        
        anim.SetBool("isDashing", isDashing);
        anim.SetBool("isAttacking",_isAttacking);
        anim.SetInteger("comboCounter", _comboCounter);
  
    }
    

    private void FlipController()
    {
        //moving right and not facing right 
        if (rb.velocity.x > 0 && !_facingRight)
        {
            Flip();
        }
        //moving left and not facing left
        else if (rb.velocity.x<0 && _facingRight) 
        {
            Flip();
        }
    }
    
    public void SetVelocity(float xVelocity, float yVelocity)
    {
        rb.velocity = new Vector2(xVelocity, yVelocity);
    }
    
}
