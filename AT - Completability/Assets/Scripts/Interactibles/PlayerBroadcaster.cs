using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBroadcaster : Broadcaster
{
    private CompletionControls controls;
    private void Awake()
    {
        controls = new();
        SetUpControls();
    }
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.GetComponent<Interactible>())
        {
            interactibles.Add(other.gameObject.GetComponent<Interactible>());
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Interactible>())
        {
            interactibles.Remove(other.gameObject.GetComponent<Interactible>());
        }
    }

    private void SetUpControls()
    {
        controls.Player.Interact.performed += ctx => Broadcast(true);
    }
    public void EnableInput()
    {
        controls.Player.Enable();
    }
    public void DisableInput()
    {
        controls.Player.Disable();
    }
    private void OnDisable()
    {
        DisableInput();
    }
    private void OnEnable()
    {
        
        EnableInput();
    }
}
