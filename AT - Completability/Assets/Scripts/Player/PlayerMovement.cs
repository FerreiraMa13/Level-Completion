using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class PlayerMovement : MonoBehaviour
{
    private CompletionControls controls;
    private CharacterController controller;
    public CinemachineVirtualCamera main_cam;
    private CinemachineTransposer transposer;
    private CinemachineComposer composer;
    private Transform cam_transform;

    private Vector2 movement_inputs = Vector2.zero;
    private Vector2 rotate_inputs = Vector2.zero;
    private float jump_velocity = 0.0f;
    private float turn_smooth_velocity;

    public float movement_speed = 2.0f;
    public int number_of_jumps = 0;
    public float jump_force = 10.0f;
    public float gravity = 9.8f;
    public float additional_decay = 0.0f;
    public float decay_multiplier = 0.2f;
    public float backwards_multiplier = -0.5f;
    public float deadzone = 0.1f;
    public float turn_smooth_time = 0.1f;

    [System.NonSerialized] public bool jumping = false;
    [System.NonSerialized] public bool landing = false;
    [System.NonSerialized] public int jump_attempts = 0;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
        composer = main_cam.GetCinemachineComponent<CinemachineComposer>();
        transposer = main_cam.GetCinemachineComponent<CinemachineTransposer>();
        cam_transform = GameObject.FindGameObjectWithTag("MainCamera").transform;

        SetUpControls();
    }
    private void FixedUpdate()
    {
        HandleMovement();
    }
    private void HandleMovement()
    {
        Vector3 input_direction = new(movement_inputs.x, 0.0f, movement_inputs.y);
        Vector3 rotation_direction = new(rotate_inputs.x, 0.0f, rotate_inputs.y);
        Vector3 rotate = RotateCalc(rotation_direction, cam_transform.transform.eulerAngles.y);
        Vector3 movement = XZMoveCalc(rotate);

        movement.y = jump_velocity;
        var movement_motion = movement_speed * Time.deltaTime * movement /** movement_inputs.y*/;
        if (movement_inputs.y < 0)
        {
            movement_motion *= backwards_multiplier;
        }
        controller.Move(movement_motion);
    }
    private Vector3 RotateCalc(Vector3 inputs, float anchor_rotation)
    {
        inputs.Normalize();
        float rotateAngle = Mathf.Atan2(inputs.x, inputs.z) * Mathf.Rad2Deg + anchor_rotation;
        float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotateAngle, ref turn_smooth_velocity, turn_smooth_time);
        transform.rotation = Quaternion.Euler(0.0f, smoothAngle, 0.0f);

        return new Vector3(0.0f, rotateAngle, 0.0f);
    }
    private Vector3 XZMoveCalc(Vector3 direction)
    {
        Vector3 forward = Quaternion.Euler(direction).normalized * Vector3.forward;
        Vector3 right = Quaternion.Euler(direction).normalized * Vector3.right;
        if (movement_inputs.y < 0)
        {
            forward = Quaternion.Euler(direction).normalized * Vector3.back;
        }
        if (movement_inputs.x < 0)
        {
            right = Quaternion.Euler(direction).normalized * Vector3.left;
        }
        Vector3 movement_y = forward * movement_speed;
        Vector3 movement_x = right * movement_speed;

        if (!Compare2Deadzone(movement_inputs.x))
        {
            movement_x = Vector3.zero;
        }
        if (!Compare2Deadzone(movement_inputs.y))
        {
            movement_y = Vector3.zero;
        }

        var movement = movement_y + movement_x;
        return movement;
    }

    private bool Compare2Deadzone(float value)
    {
        if (value < deadzone)
        {
            if (value > -deadzone)
            {
                return false;
            }
        }
        return true;
    }
    private void SetUpControls()
    {
        controls = new();
        controls.Player.Move.performed += ctx => movement_inputs = ctx.ReadValue<Vector2>();
        controls.Player.Move.canceled += ctx => movement_inputs = Vector2.zero;
        /*controls.Player.Look.performed += ctx => rotate_inputs = ctx.ReadValue<Vector2>();
        controls.Player.Look.canceled += ctx => rotate_inputs = Vector2.zero;*/
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
