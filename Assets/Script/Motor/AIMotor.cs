using UnityEngine;
using System.Collections;

public class AIMotor : BaseMotor
{
    #region Fields
    Vector3 destination = Vector3.zero;
    #endregion

    #region Init & Update
    protected override void Start()
    {
        base.Start();

        state = gameObject.AddComponent<AIWalkingState>();
        state.Construct();
    }

    protected override void UpdateMotor()
    {
        // Get the input
        MoveVector = Direction();

        // Send the input to a filter
        MoveVector = state.ProcessMotion(MoveVector);
        RotationQuaternion = state.ProcessRotation(MoveVector);

        // Check if we need to change current state
        state.Transition();

        // Move
        Move();

        Grounded();
    }
    #endregion

    #region Functions
    public void SetDestination(Transform t)
    {
        destination = t.position;
    }

    public Vector3 Direction()
    {
        if (destination == Vector3.zero)
            return Vector3.zero;
        Vector3 dir = destination - thisTransform.position;
        dir.Set(dir.x, 0, dir.z);
        return dir.normalized;
    }
    #endregion
}