using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Skeletion : Entity
{
    private bool _isAttackting;
    
    [Header("Move info")]
    [SerializeField] private float  moveSpeed;
    
    [Header("Player detection")]
    [SerializeField] protected float playerCheckDistance;
    [SerializeField] protected LayerMask whatIsPlayer;
    protected RaycastHit2D _isPlayerDetected;
    

    // Start is called before the first frame update
    protected override void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    protected override void Update()
    {
        base.Update();
        if (_isPlayerDetected)
        {
            if (_isPlayerDetected.distance > 1)
            {
                _rb.velocity = new Vector2(moveSpeed *1.5f* _facingDirection, _rb.velocity.y);
                Debug.Log("i see the player");
                _isAttackting = false;
            }
            else
            {
                Debug.Log("attack!" + _isPlayerDetected.collider.gameObject.name);
                _isAttackting = true;
            }
        }
        if (!_isGrounded|| _isWallDetected)
        {
            Flip();
        }

        Movement();
    }

    private void Movement()
    {
        if (!_isAttackting)
        {
            _rb.velocity = new Vector2(moveSpeed * _facingDirection, _rb.velocity.y); 
        }
    }

    protected override void CollisionChecks()
    {
        base.CollisionChecks();
        _isPlayerDetected = Physics2D.Raycast(transform.position, Vector2.right, playerCheckDistance * _facingDirection,
            whatIsPlayer);

    }

    protected override void OnDrawGizmos()
    {
        base.OnDrawGizmos();
        
        Gizmos.color = Color.blue;
        Gizmos.DrawLine(transform.position,new Vector3(transform.position.x + playerCheckDistance* _facingDirection,transform.position.y));
    }
}
