using UnityEngine;
using System.Collections;
using System;

public class PlayerMotor : BaseMotor
{
    CameraMotor camMotor;

    protected override void Start()
    {
        base.Start();

        camMotor = gameObject.AddComponent<CameraMotor>();
        camMotor.Init();
    }

    protected override void UpdateMotor()
    {
        // Get the input
        MoveVector = InputDirection();

        // Send the input to a filter
        MoveVector = state.ProcessMotion(MoveVector);

        // Check if we need to change current state
        state.Transition();

        // Move
        Move();

        Grounded();
    }

    Vector3 InputDirection()
    {
        var dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();

        return dir;
    }
}
