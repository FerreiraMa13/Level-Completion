using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorMovement : Effect
{
    public GameObject door;

    public Vector3 door_movement = Vector3.zero;
    private Vector3 end_point = Vector3.zero;
    private Vector3 start_point = Vector3.zero;

    public float opening_speed = 1;
    public float error_margin = 0.5f;

    private bool active_movement = false;
    private bool forward = true;

    private void Awake()
    {
        end_point = door.transform.position + door_movement;
        start_point = door.transform.position;
    }
    private void FixedUpdate()
    {
        if(valid)
        {
            HandleDoorMovement();
        }
    }
    public override void Activate()
    {
        if(valid)
        {
            if (!active_movement)
            {
                active_movement = true;
                forward = !forward;
            }
        }
    }
    private void HandleDoorMovement()
    {
        if(active_movement)
        {
            var direction = GetDirection();
            door.transform.position += direction * opening_speed * Time.deltaTime;
            CheckDestination();
        }
    }
    private Vector3 GetDirection()
    {
        if(forward)
        {
            return end_point - door.transform.position;
        }
        else
        {
            return start_point - door.transform.position;
        }
    }
    private void CheckDestination()
    {
        Vector3 destination;
        if (forward) { destination = end_point; }
        else { destination = start_point; }
        if((destination - door.transform.position).magnitude <= error_margin)
        {
            door.transform.position = destination;
            active_movement = false;
        }
    }
}
