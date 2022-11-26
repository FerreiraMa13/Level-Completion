using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class Check_AI : MonoBehaviour
{
    private NavMeshAgent agent = new();

    private void Awake()
    {
        agent = GetComponent<NavMeshAgent>();
    }
    public void SetDestination(Vector3 new_destination)
    {
        agent.SetDestination (new_destination);
    }
}
