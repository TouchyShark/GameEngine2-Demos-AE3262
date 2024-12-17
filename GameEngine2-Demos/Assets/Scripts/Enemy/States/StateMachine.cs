using System;
using System.Collections;
using UnityEngine;

public class StateMachine : MonoBehaviour
{
    public BaseState activeState;

    private void Start()
    {
        // Initialize the state machine with PatrolState
        ChangeState(new PatrolState());
    }

    private void Update()
    {
        if (activeState != null)
        {
            activeState.Perform();
        }
    }

    public void ChangeState(BaseState newState)
    {
        // Clean up the current state
        if (activeState != null)
        {
            activeState.Exit();
        }

        // Change to the new state
        activeState = newState;

        if (activeState != null)
        {
            activeState.stateMachine = this;
            activeState.enemy = GetComponent<Enemy>();
            activeState.Enter();
        }
    }

    internal void Initialise()
    {
        throw new NotImplementedException();
    }
}
