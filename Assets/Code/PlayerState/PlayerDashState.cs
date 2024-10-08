using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDashState : PlayerState
{
    public PlayerDashState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        stateTimer = player.dashDuration;
    }

    public override void Update()
    {
        base.Update();
        if (!player.isGroundDetected() && player.isWallDetected())
        {
            stateMachine.ChangeState(player.wallSlideState);
        }
        player.SetVelocity(player.dashSpeed*player.dashDirection,0);
        if (stateTimer < 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        player.SetVelocity(0, rb.velocity.y);
    }
}
