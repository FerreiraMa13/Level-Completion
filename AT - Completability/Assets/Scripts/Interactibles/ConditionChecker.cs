using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionChecker : MonoBehaviour
{
    public List<Condition> conditions = new();
    public List<Effect> effects = new();
    public bool ongoing = false;
    public bool achieved = false;

    private bool past_activation = false;
    private void Awake()
    {
        var temp_conditions = GetComponents<Condition>();
        foreach(var condition in temp_conditions)
        {
            if(!conditions.Contains(condition))
            {
                conditions.Add(condition);
            }
        }
        var temp_effects = GetComponents<Effect>();
        foreach(var effect in temp_effects)
        {
            if(!effects.Contains(effect))
            {
                effects.Add(effect);
            }
        }
    }

    private void Update()
    {
        achieved = CheckConditions();
        if (achieved != past_activation)
        {
            foreach(var effect in effects)
            {
                effect.Activate();
                Debug.Log("Effect Activate");
            }
            past_activation = achieved;
        }
    }
    private bool CheckConditions()
    {
        if(ongoing || !achieved)
        {
            foreach (var condition in conditions)
            {
                if (!condition.CheckCondition())
                {
                    return false;
                }
            }
        }
        return true;
    }
}
