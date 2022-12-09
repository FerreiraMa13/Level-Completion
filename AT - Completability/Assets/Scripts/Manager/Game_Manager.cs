using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : MonoBehaviour
{
    private Text_Manager text_manager;
    public bool perform_check = true;
    private Check_AI minion;
    private Checkpoint goal;
    private PlayerMovement player;

    public string minion_tag;
    public string goal_tag = "Goal";
    public bool tick = false;
    public List<Checkpoint> checkpoints = new();

    private void Awake()
    {
        text_manager = GetComponent<Text_Manager>();
        minion = GameObject.FindGameObjectWithTag(minion_tag).GetComponent<Check_AI>();
        goal = GameObject.FindGameObjectWithTag(goal_tag).GetComponent<Checkpoint>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();

        if (perform_check)
        {
            checkpoints.Add(goal);
            player.DisableInput();
        }
    }
    private void Start()
    {
        tick = true;
        minion.manager = this;
    }
    private void Update()
    {
        if (tick)
        {
            if (checkpoints.Count > 0)
            {
                minion.SetDestination(checkpoints[0].gameObject.transform.position);
            }
            tick = false;
        }
    }
    public void RequestTick()
    {
        tick = true;
    }
    public void RequestNotComplete(string name)
    {
        if(text_manager)
        {
            text_manager.ErrorMessageCNR(name);
        }
        else
        {
            Debug.Log(name);
        }
    }
    public void RemoveFirst()
    {
        if (checkpoints.Count > 0)
        {
            checkpoints.RemoveAt(0);
        }
        else
        {
            GoalReached();
        }
    }
    public bool CheckAchieved()
    {
        if(checkpoints.Count > 1)
        {
            return checkpoints[0].achieved;
        }
        return true;
    }
    public void AddAt(int index, Checkpoint transform)
    {
        if (!checkpoints.Contains(transform))
        {
            checkpoints.Insert(index, transform);
        }
    }
    public void GoalReached()
    {
        text_manager.EndToggle();
        player.EnableInput();
        minion.valid = false;
        minion.enabled = false;
        tick = false;
    }
}
