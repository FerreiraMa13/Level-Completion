using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Signal_Manager : MonoBehaviour
{
    [SerializeField] private Dictionary<int, bool> signals = new();
    private void Awake()
    {
        for(int key = 0; key < 10; key++)
        {
            signals.Add(key, false);
        }
    }
    public bool ListenToChannel (int key)
    {
        return signals[key];
    }
    public void OverrideChannel (int key, bool new_state)
    {
        signals[key] = new_state;
    }
    public void ToggleChannel (int key)
    {
        var state = signals[key];
        signals[key] = !state;
    }
}
