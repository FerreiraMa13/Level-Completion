using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactible
{
    public List<Condition> conditions = new();
    public Effect effect;

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
        if (effect == null)
        {
            effect = GetComponent<Effect>();
        }
    }
    public override void Interact()
    {
        if(CheckConditions())
        {
            effect.Activate();
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
