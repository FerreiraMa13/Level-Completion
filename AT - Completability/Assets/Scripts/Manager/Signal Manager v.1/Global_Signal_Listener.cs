using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global_Signal_Listener : Condition
{
    [SerializeField] private Signal_Manager signal_manager;
    public List<int> channels = new();
    private void Awake()
    {
        SignalSetUP();
    }
    override protected bool CheckRequirments()
    {
        if(channels.Count < 1)
        {
            return true;
        }
        else
        {
            foreach(var channel in channels)
            {
                if(!signal_manager.ListenToChannel(channel))
                {
                    return false;
                }
            }
            return true;
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
