using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class PlayerProximity : Condition
{
    public bool player_proximity = false;
    protected override bool CheckRequirments()
    {
        return player_proximity;
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            player_proximity = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            player_proximity = false;
        }
    }
}
