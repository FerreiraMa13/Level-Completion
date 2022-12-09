using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Checkpoint : MonoBehaviour
{
    public List<Checkpoint> required_chechpoints = null;
    public bool accounted = false;
    public bool achieved = false;

    private void Update()
    {
        if(!achieved)
        {
            achieved = CheckRequirements();
        }
    }
    private bool CheckRequirements()
    {
        foreach(var req in required_chechpoints)
        {
            if(!req.accounted)
            {
                return false;
            }
        }
        return true;
    }
}
