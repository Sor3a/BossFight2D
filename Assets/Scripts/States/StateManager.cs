using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager 
{
    State currentState;
    Dictionary<State, List<Transition>> stateTransitions;
    public StateManager(State startingState)
    {
        currentState = startingState;
        currentState.StartState();
        stateTransitions = new Dictionary<State, List<Transition>>();
    }
    public State GetCurrentState() { return currentState; }
    public void AddStateTransition(State state, Transition transition)
    {
        if (!stateTransitions.ContainsKey(state))
        {
            stateTransitions.Add(state, new List<Transition>());
        }
        stateTransitions[state].Add(transition);
    }
    public void ChangeState(State newState)
    {
        if (currentState is DeathState) return;
        if(GameManager.instance.GameState == GameStates.Finished && !(newState is IdleState))
            return;
        currentState.EndState();
        currentState = newState;
        currentState.StartState();
    }
    public void UpdateStates(float deltaTime)
    {
        if (currentState is DeathState) return;
        currentState.UpdateState(deltaTime);
        List<Transition> stateTransitionsList = stateTransitions[currentState];
        if (stateTransitionsList == null || stateTransitionsList.Count == 0) return;
        foreach (var transition in stateTransitionsList)
        {
            if (transition.IsConditionMet())
            {
                ChangeState(transition.GetState());
                break;
            }
        }

    }
}
