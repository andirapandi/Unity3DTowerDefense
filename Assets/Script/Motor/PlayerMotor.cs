using UnityEngine;
using System.Collections;
using System;

public class PlayerMotor : BaseMotor
{
    CameraMotor camMotor;
    Transform camTransform;

    protected override void Start()
    {
        base.Start();

        camMotor = gameObject.AddComponent<CameraMotor>();
        camMotor.Init();
        camTransform = camMotor.CameraContainer;
    }

    protected override void UpdateMotor()
    {
        // Get the input
        MoveVector = InputDirection();

        // Rotate out MoveVector with Camera's forward
        MoveVector = RotateWithView(MoveVector);

        // Send the input to a filter
        MoveVector = state.ProcessMotion(MoveVector);
        RotationQuaternion = state.ProcessRotation(MoveVector);

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

    Vector3 RotateWithView(Vector3 input)
    {
        Vector3 dir = camTransform.TransformDirection(input);
        dir.Set(dir.x, 0, dir.z); // no movement in y
        return dir.normalized * input.magnitude;
    }
}
