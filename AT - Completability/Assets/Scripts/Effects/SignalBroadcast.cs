using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalBroadcast : Effect
{
    public bool signal = false;

    public override void Activate()
    {
        if (valid)
        {
            signal = !signal;
        }
    }
}
