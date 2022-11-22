using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Broadcaster : MonoBehaviour
{
    public bool valid = true;
    public bool active = true;
    public List<Interactible> interactibles = new();

    public int Broadcast(bool signal)
    {
        if(valid && active)
        {
            int index = 0;
            foreach (var interact in interactibles)
            {
                index++;
                interact.Interact();
            }
            return index;
        }
        return -1;
    }
    public void Add (Interactible new_interact)
    {
        if(valid)
        {
            if (!interactibles.Contains(new_interact))
            {
                interactibles.Add(new_interact);
            }
        }
    }
    public void Remove(Interactible new_interact)
    {
        if(valid)
        {
            if (interactibles.Contains(new_interact))
            {
                interactibles.Remove(new_interact);
            }
        }
    }
}
