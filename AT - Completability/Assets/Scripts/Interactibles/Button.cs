using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactible
{
    public List<Condition> conditions = new();
    public List<Effect> effects;

    private void Awake()
    {
        var temp_conditions = GetComponents<Condition>();
        foreach (var condition in temp_conditions)
        {
            if (!conditions.Contains(condition))
            {
                conditions.Add(condition);
            }
        }
        var temp_effects = GetComponents<Effect>();
        foreach(var effect in temp_effects)
        {
            if (!effects.Contains(effect))
            {
                effects.Add(effect);
            }
        }
    }
    public override void Interact()
    {
        if(CheckConditions())
        {
            foreach(var effect in effects)
            {
                effect.Activate();
            }
        }
    }

    public bool CheckConditions()
    {
        foreach(var condition in conditions)
        {
            if(!condition.CheckCondition())
            {
                return false;
            }
        }
        return true;
    }
}
