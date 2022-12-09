using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Check_AI : MonoBehaviour
{
    public Game_Manager manager;
    private NavMeshAgent agent = new();
    public Vector3 destination;
    public float patient = 1.0f;
    public float patience_timer = 1.0f;
    public float objective_treshhold = 0.5f;
    public float stopped_step_treshold = 0.05f;
    public Vector3 past_position;
    public bool valid = true;


    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
        destination = Vector3.zero;
    }
    private void FixedUpdate()
    {
        if (valid)
        {
            var difference = transform.position - past_position;
            if (difference.magnitude <= stopped_step_treshold || agent.isStopped)
            {
                patience_timer -= Time.deltaTime;
                if (patience_timer <= 0)
                {
                    var destination_dif = destination - transform.position;
                    if (destination_dif.magnitude <= objective_treshhold)
                    {
                        if (manager.CheckAchieved())
                        {
                            manager.RemoveFirst();
                            manager.RequestTick();
                            patience_timer = patient;
                        }
                        else
                        {
                            RequestNotComplete(manager.checkpoints[0].name);
                        }
                    }
                    else
                    {
                        RequestNotComplete("stuck on geometry");
                    }
                }
            }
            else
            {
                patience_timer = patient;
            }
            past_position = transform.position;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        var script = other.gameObject.GetComponent<Checkpoint>();
        if (script != null)
        {
            if (!script.accounted)
            {
                Debug.Log("UpdateCheckpoints");
                UpdateCheckpoints(script);
            }
        }
    }

    public void SetDestination(Vector3 new_destination)
    {
        agent.SetDestination(new_destination);
        destination = new_destination;
    }
    private void UpdateCheckpoints(Checkpoint script)
    {
        if (script.required_chechpoints != null)
        {
            manager.AddAt(0, script);
            int checkpoint_index = 0;
            foreach (var requirement in script.required_chechpoints)
            {
                if(!manager.checkpoints.Contains(requirement))
                {
                    manager.AddAt(checkpoint_index, requirement);
                    checkpoint_index++;
                }
            }
        }
        else
        {
            if(!script.achieved)
            {
                RequestNotComplete(script.gameObject.name);
            }
        }
        script.accounted = true;
        
        RequestTick();
    }
    private void RequestTick()
    {
        if (!manager.tick){manager.RequestTick();}
    }
    private void RequestNotComplete(string name)
    {
        manager.RequestNotComplete(name);
    }
}
