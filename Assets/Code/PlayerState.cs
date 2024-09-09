using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;

    private string animBoolName;

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
    }


    public virtual void Enter()
    {
        Debug.Log("I enter " + animBoolName);
    }
    public virtual void Update()
    {
        Debug.Log("I in " + animBoolName);
    }
    public virtual void Exit()
    {
        Debug.Log("I exit " + animBoolName);
    }
}
