using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private Check_AI minion;
    private GameObject goal;

    public string minion_tag;
    public string goal_tag = "Goal";
    public bool tick = false;

    private void Awake()
    {
        minion = GameObject.FindGameObjectWithTag(minion_tag).GetComponent<Check_AI>();
        goal = GameObject.FindGameObjectWithTag(goal_tag);
    }

    private void Update()
    {
        if(tick)
        {
            minion.SetDestination(goal.transform.position);
            tick = false;
        }
    }
}
