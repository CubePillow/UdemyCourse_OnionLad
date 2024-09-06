using UnityEngine;

public class Player : MonoBehaviour 
{
    private Rigidbody2D _rb;
    private Animator _anim;
    [SerializeField] private float  moveSpeed;
    [SerializeField] private float jumpForce;

    [Header("Dash Info")] 
    [SerializeField] private float dashDuration;
    [SerializeField] private float dashTime;
    [SerializeField] private float  dashSpeed;
    

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
        dashTime -= Time.deltaTime; //Time.deltaTime: time from last frame 

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            dashTime = dashDuration;
        }
        if (dashTime > 0)
        {
            Debug.Log("doing dash ability");
        }
        
        FlipController();
        AnimatorControllers();

    }

    private void CollisionChecks()
    {
        _isGrounded= Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance,whatIsGround);
    }

    private void CheckInput()
    {
        _xInput = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    private void Movement()
    {
        if (dashTime > 0)
        {
            _rb.velocity = new Vector2(_xInput * dashSpeed,0);
        }
        else
        {
            _rb.velocity = new Vector2(_xInput * moveSpeed,_rb.velocity.y);
        }
        
    }

    private void Jump()
    {
        if (_isGrounded)
        { 
            _rb.velocity = new Vector2(_rb.velocity.x, jumpForce);
        }
    }

    private void AnimatorControllers()
    {
        bool isMoving = _rb.velocity.x !=0;
        bool isDashing = dashTime > 0;
        
        _anim.SetFloat("yVelocity", _rb.velocity.y);
        _anim.SetBool("isMoving", isMoving);
        _anim.SetBool("isGrounded", _isGrounded);
        _anim.SetBool("isDashing", isDashing);
  
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
