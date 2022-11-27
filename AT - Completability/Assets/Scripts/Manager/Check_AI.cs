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
        if(valid)
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
                        manager.RemoveFirst();
                        manager.RequestTick();
                        patience_timer = patient;
                    }
                    else
                    {
                        Debug.Log("No movement available");
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
    public void SetDestination(Vector3 new_destination)
    {
        agent.SetDestination (new_destination);
        destination = new_destination;
    }
    private void OnTriggerEnter(Collider other)
    {
        var script = other.gameObject.GetComponent<Checkpoint>();
        if(script != null)
        {
            if(!script.accounted)
            {
                if (script.required_chechpoint != null)
                {
                    manager.AddAt(0, script.required_chechpoint.transform);
                    manager.AddAt(1, other.transform);
                }
                script.accounted = true;
                if(!manager.tick)
                {
                    manager.RequestTick();
                }
            }
        }
    }
}
