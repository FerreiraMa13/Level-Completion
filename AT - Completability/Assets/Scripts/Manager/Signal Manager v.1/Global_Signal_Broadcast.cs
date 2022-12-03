using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Signal_Broadcast : Effect
{
    [Space(5)]
    [Header("General")]
    [SerializeField] private Signal_Manager signal_manager;
    public bool force_state = false;
    public List<int> channels = new();
    [Space(5)]
    [Header("Forced State")]
    public bool state = false;

    private void Awake()
    {
        SignalSetUP();
    }
    public override void Activate()
    {
        if (valid)
        {
            if(!force_state)
            {
                foreach(var channel in channels)
                {
                    signal_manager.ToggleChannel(channel);
                }
            }
            else
            {
                foreach (var channel in channels)
                {
                    signal_manager.OverrideChannel(channel, state);
                }
            }
        }
    }
    private void SignalSetUP()
    {
        signal_manager = GameObject.FindGameObjectWithTag("Manager").GetComponent<Signal_Manager>();
        if (channels.Count < 1)
        {
            channels.Add(0);
        }
    }
}
