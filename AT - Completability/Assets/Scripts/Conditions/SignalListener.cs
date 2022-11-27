using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalListener : Condition
{
    public List<SignalBroadcast> signals = new();

    private void Awake()
    {
        if(signals.Count < 1)
        {
            var self_signals = GetComponents<SignalBroadcast>();
            foreach(var signal in self_signals)
            {
                signals.Add(signal);
            }
        }
    }

    override protected bool CheckRequirments()
    {
        if(signals.Count < 1)
        {
            return true;
        }
        else
        {
            foreach(var broadcast in signals)
            {
                if(!broadcast.signal)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
