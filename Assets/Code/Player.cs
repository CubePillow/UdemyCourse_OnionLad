using UnityEngine;

public class Player : Entity 
{
    [Header("Move Info")] 
    [SerializeField] private float  moveSpeed;
    [SerializeField] private float jumpForce;

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
    
    

    private float _xInput;
   
    

    protected override void Start()
    {
        base.Start();
        
    }

    // Update is called once per frame
    protected override void Update()
    {  
        base.Update();
        Movement();
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
        _xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            StartAttackEvent();
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
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
            _rb.velocity = new Vector2(0,0);
        }
        else if (_dashTime > 0)
        {
            _rb.velocity = new Vector2(_facingDirection * dashSpeed,0);
        }
        else
        {
            _rb.velocity = new Vector2(_xInput * moveSpeed,_rb.velocity.y);
        }
        
    }

    private void Jump()
    {
        if (_isGrounded && !_isAttacking)
        { 
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = _rb.velocity.x !=0;
        bool isDashing = _dashTime > 0;
        
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("isMoving", isMoving);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("isDashing", isDashing);
        _anim.SetBool("isAttacking",_isAttacking);
        _anim.SetInteger("comboCounter", _comboCounter);
  
    }
    

    private void FlipController()
    {
        //moving right and not facing right 
        if (_rb.velocity.x > 0 && !_facingRight)
        {
            Flip();
        }
        //moving left and not facing left
        else if (_rb.velocity.x<0 && _facingRight) 
        {
            Flip();
        }
    }
    
}
