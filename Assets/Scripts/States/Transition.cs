using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Transition 
{
    Func<bool> condition;
    List<StatePourcentage> availableStates;
    public Transition(Func<bool> condition,params StatePourcentage[] statePourcentage)
    {
        this.condition = condition;
        availableStates = new List<StatePourcentage>();
        for (int i = 0; i < statePourcentage.Length; i++)
        {
            availableStates.Add(statePourcentage[i]);
        }
       
    }
    public State GetState()
    {
        float percentage = UnityEngine.Random.Range(0,100); 
        float currentPercentage =0;
        foreach (var statePer in availableStates) 
        {
            currentPercentage += statePer.percentage;
            if(percentage <= currentPercentage)
            {
                return statePer.state;
            }
        }
        return null;
    }
    public bool IsConditionMet()
    {
        return condition();
    }
}

public class StatePourcentage
{
    public State state;
    public float percentage;
    public StatePourcentage(State state,float percentage)
    {
        this.state = state;
        this.percentage = percentage;
    }
}
