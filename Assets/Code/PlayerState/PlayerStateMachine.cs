using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStateMachine 
{
    public PlayerState currentState { get; private set; } //public -> get it, private -> change it (read only)

    public void Initialize(PlayerState startState)
    {
        currentState = startState;
        currentState.Enter();
    }
    
    public void ChangeState(PlayerState newState)
    {
        currentState.Exit();
        currentState = newState;
        currentState.Enter();
    }
}