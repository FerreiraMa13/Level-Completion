using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Condition : MonoBehaviour
{
    public bool valid = true;
    public bool achieved = true;
    public bool ongoing = false;

    public bool CheckCondition()
    {
        if (ongoing)
        {
            return CheckRequirments();
        }
        else
        {
            if (!achieved)
            {
                achieved = CheckRequirments();
            }

            return achieved;
        }
    }
    virtual protected bool CheckRequirments()
    {
        return false;
    }
}
