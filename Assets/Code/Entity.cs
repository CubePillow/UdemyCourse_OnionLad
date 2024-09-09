using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entity : MonoBehaviour
{
    protected Rigidbody2D _rb;
    protected Animator _anim;
    
    protected int _facingDirection = 1;
    protected bool _facingRight = true;

    [Header("Collision info")] 
    [SerializeField] protected Transform groundCheckpoint;
    [SerializeField] protected float groundCheckDistance;
    [SerializeField] protected LayerMask whatIsGround; 
    protected bool _isGrounded;
    
    // Start is called before the first frame update
    protected virtual void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponentInChildren<Animator>();

    }

    // Update is called once per frame
    protected virtual void Update()
    {
        CollisionChecks();
    }
    
    protected virtual void CollisionChecks()
    {
        _isGrounded= Physics2D.Raycast(groundCheckpoint.position, Vector2.down, groundCheckDistance,whatIsGround);
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
    }
}
