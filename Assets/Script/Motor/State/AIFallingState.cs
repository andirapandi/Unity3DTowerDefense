using UnityEngine;
using System.Collections;

public class AIFAllingState : FallingState {
    public override void Transition()
    {
        if (motor.Grounded())
            motor.ChangeState("AIWalkingState");
    }
}
