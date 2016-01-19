using UnityEngine;
using System.Collections;

public abstract class BaseState : MonoBehaviour
{
    protected BaseMotor motor;

    #region baseState implementation

    public virtual void Construct()
    {
        motor = GetComponent<BaseMotor>();
    }

    public virtual void Destruct()
    {
        Destroy(this);
    }

    // HandleTransition
    public virtual void Transition()
    {

    }

    #endregion

    public abstract Vector3 ProcessMotion(Vector3 input);
    public virtual Quaternion ProcessRotation(Vector3 input)
    {
        return transform.rotation;
    }

    protected void ApplySpeed(ref Vector3 input, float speed)
    {
        input *= speed;
    }

    protected void ApplyGravity(ref Vector3 input, float gravity)
    {
        motor.VerticalVelocity -= gravity * Time.deltaTime;
        motor.VerticalVelocity = Mathf.Clamp(motor.VerticalVelocity, -motor.TerminalVelocity, motor.TerminalVelocity);
        input.Set(input.x, motor.VerticalVelocity, input.z);
    }
}
