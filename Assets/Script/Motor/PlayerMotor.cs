using UnityEngine;
using System.Collections;
using System;

public class PlayerMotor : BaseMotor
{
    protected override void UpdateMotor()
    {
        // Get the input
        MoveVector = InputDirection();

        // Send the input to a filter
        MoveVector = state.ProcessMotion(MoveVector);

        // Check if we need to change current state
        state.HandleTransition();

        // Move
        Move();

        CheckGrounded();
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
