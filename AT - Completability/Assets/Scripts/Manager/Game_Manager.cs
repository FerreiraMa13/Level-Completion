using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private Check_AI minion;
    private GameObject goal;
    private PlayerMovement player;

    public string minion_tag;
    public string goal_tag = "Goal";
    public bool tick = false;
    public List<Transform> checkpoints = new();

    private void Awake()
    {
        minion = GameObject.FindGameObjectWithTag(minion_tag).GetComponent<Check_AI>();
        goal = GameObject.FindGameObjectWithTag(goal_tag);

        checkpoints.Add(goal.transform);
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        player.DisableInput();
    }
    private void Start()
    {
        tick = true;
        minion.manager = this;
    }
    private void Update()
    {
        if(tick)
        {
            if(checkpoints.Count > 0)
            {
                minion.SetDestination(checkpoints[0].position);
            }
            tick = false;
        }
    }

    public void RequestTick()
    {
        tick = true;
    }

    public void RemoveFirst()
    {
        if(checkpoints.Count > 0)
        {
            checkpoints.RemoveAt(0);
        }
        else
        {
            GoalReached();
        }
    }
    public void AddAt(int index, Transform transform)
    {
        if(!checkpoints.Contains(transform))
        {
            checkpoints.Insert(index, transform);
        }
    }
    public void GoalReached()
    {
        player.EnableInput();
        minion.valid = false;
        minion.enabled = false;
        tick = false;
    }
}
