using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerState
{
    protected PlayerStateMachine stateMachine;
    protected Player player;
    protected Rigidbody2D rb;

    private string animBoolName;
    protected float xInput;
    protected float yInput;
    protected float stateTimer;

    protected bool isTiggerCalled;

    public PlayerState(PlayerStateMachine stateMachine, Player player, string animBoolName)
    {
        this.stateMachine = stateMachine;
        this.player = player;
        this.animBoolName = animBoolName;
        isTiggerCalled = false;
    }


    public virtual void Enter()
    {
        Debug.Log("I enter " + animBoolName);
        player.anim.SetBool(animBoolName, true);
        rb = player.rb;
        isTiggerCalled = false;
    }
    public virtual void Update()
    {
        Debug.Log("I in " + animBoolName);
        stateTimer -= Time.deltaTime; //Time.deltaTime: time from last frame
        xInput = Input.GetAxisRaw("Horizontal");
        yInput = Input.GetAxisRaw("Vertical");
        player.anim.SetFloat("yVelocity", rb.velocity.y);
       
        
    }
    public virtual void Exit()
    {
        Debug.Log("I exit " + animBoolName);
        player.anim.SetBool(animBoolName, false);
    }
    
    public virtual void AnimationFinitionTigger()
    {
        isTiggerCalled = true;
    }
}
