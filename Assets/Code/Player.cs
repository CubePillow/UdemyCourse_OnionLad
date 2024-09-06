using UnityEngine;

public class Player : MonoBehaviour 
{
    private Rigidbody2D _rb;
    private Animator _anim;
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
    private int _facingDirection = 1;
    private bool _facingRight = true;

    [Header("Collision info")]
    [SerializeField] private float groundCheckDistance;
    [SerializeField] private LayerMask whatIsGround; 
    private bool _isGrounded; 
    
   
    
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {  
        Movement();
        CheckInput();

        CollisionChecks();
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

    private void CollisionChecks()
    {
        _isGrounded= Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance,whatIsGround);
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

    private void Flip()
    {
        _facingDirection = _facingDirection * -1;
        _facingRight = !_facingRight; 
        transform.Rotate(0,180,0);
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

    private void OnDrawGizmos()
    {
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x,transform.position.y-groundCheckDistance,whatIsGround));
    }
}
