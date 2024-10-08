using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerWallSlideState : PlayerState
{
    public PlayerWallSlideState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        
    }

    public override void Update()
    {
        base.Update();
        //wall jump 
        if (Input.GetKeyDown(KeyCode.Space))
        {
            stateMachine.ChangeState(player.wallJumpState);
            return;
        }
        
        if (xInput != 0 && player.facingDirection != xInput)
        { 
            stateMachine.ChangeState(player.idleState);
            
        }

        if (yInput < 0)
        {
            player.SetVelocity(0, rb.velocity.y);
        }else
        {
            player.SetVelocity(0, rb.velocity.y*0.7f);
        }
        

        if (player.isGroundDetected())
        {
            stateMachine.ChangeState(player.idleState);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
