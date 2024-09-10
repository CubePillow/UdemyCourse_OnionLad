using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerPrimaryAttackState : PlayerState
{
    private int comboCounter;
    private float lastTimeAttack;
    
    public PlayerPrimaryAttackState(PlayerStateMachine stateMachine, Player player, string animBoolName) : base(stateMachine, player, animBoolName)
    {
    }

    public override void Enter()
    {
        base.Enter();
        if (comboCounter > 2 || Time.time >= lastTimeAttack + player.comboWindow)
        {
            comboCounter = 0;
        }
        player.anim.SetInteger("comboCounter",comboCounter);
        Debug.Log(comboCounter);
    }

    public override void Update()
    {
        base.Update();
        if (isTiggerCalled)
        {
            stateMachine.ChangeState(player.idleState);
        }
    }

    public override void Exit()
    {
        base.Exit();
        comboCounter++;
        lastTimeAttack = Time.time;
    }
}
