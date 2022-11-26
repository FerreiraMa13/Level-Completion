using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node : MonoBehaviour
{
    public enum DIRECTION
    {
        UP = 0,
        FORWARD = 1,
        RIGHT = 2,
        BACK = 3,
        LEFT = 4,
        DOWN = 5,
        UNKNOWN = -1
    }
    public GameObject game_object;
    public Vector3 key_position;
    public Dictionary<DIRECTION, Node> neighbours = new();

    public bool AddNeightbour(DIRECTION direction, Node new_neighboor)
    {
        if(direction == DIRECTION.UNKNOWN)
        {
            return false;
        }
        if(!CheckNeighbours(direction))
        {
            neighbours.Add(direction, new_neighboor);
        }
        return true;
    }
    public bool CheckNeighbours(DIRECTION direction)
    {
        if(neighbours.ContainsKey(direction))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
