using UnityEngine;
using System.Collections;

public class FallingState : BaseState
{
    public override Vector3 ProcessMotion(Vector3 input)
    {
        ApplySpeed(ref input, motor.Speed);
        ApplyGravity(ref input, motor.Gravity);
        return input;
    }

    public override void HandleTransition()
    {
        if (motor.CheckGrounded())
            motor.ChangeState("WalkingState");
    }
}
