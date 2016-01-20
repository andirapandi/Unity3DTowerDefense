using UnityEngine;
using System.Collections;

public class AIWalkingState : WalkingState {

    public override void Transition()
    {
        if (!motor.Grounded())
            motor.ChangeState("FallingState");
    }
}
