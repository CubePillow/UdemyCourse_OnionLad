using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveState : PlayerState
{
    public PlayerMoveState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
    }

    public override void Update()
    {
        base.Update();
        player.SetVelocity(_xInput*player.moveSpeed,rb.velocity.y);
        if (_xInput == 0)
        {
            stateMachine.ChangeState(player.idleState);
        }
        
    }

    public override void Exit()
    {
        base.Exit();
    }
}
