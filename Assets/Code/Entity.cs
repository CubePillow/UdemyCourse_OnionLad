using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    #region Components

    public Rigidbody2D rb { get; private set; }
    public Animator anim { get; private set; }

    #endregion
    

    protected int _facingDirection = 1;
    protected bool _facingRight = true;

    [Header("Collision info")] 
    [SerializeField] protected Transform groundCheckpoint;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround;
    [Space]
    [SerializeField] protected Transform wallCheckpoint;
    [SerializeField] protected float wallCheckDistance;
    
    public bool _isGrounded { get; private set; }
    protected bool _isWallDetected;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponentInChildren<Animator>();

        if (wallCheckpoint == null)
        {
            wallCheckpoint = transform;
        }

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }
    
    protected virtual void CollisionChecks()
    {
        _isGrounded= Physics2D.Raycast(groundCheckpoint.position, Vector2.down, groundCheckDistance,whatIsGround);
        _isWallDetected =  Physics2D.Raycast(wallCheckpoint.position, Vector2.right, wallCheckDistance,whatIsGround);
    }
    
    protected virtual void Flip()
    {
        _facingDirection = _facingDirection * -1;
        _facingRight = !_facingRight; 
        transform.Rotate(0,180,0);
    }
    
    protected virtual void OnDrawGizmos()
    {
        Gizmos.DrawLine(groundCheckpoint.position,new Vector3(groundCheckpoint.position.x,groundCheckpoint.position.y-groundCheckDistance));
        Gizmos.DrawLine(wallCheckpoint.position,new Vector3(wallCheckpoint.position.x + wallCheckDistance*_facingDirection,wallCheckpoint.position.y));
    }
    
}
