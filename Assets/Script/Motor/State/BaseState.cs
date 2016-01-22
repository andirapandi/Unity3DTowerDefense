using UnityEngine;
using System.Collections;

public abstract class BaseState : MonoBehaviour
{
    #region Fields
    protected BaseMotor motor;
    #endregion

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

    public abstract Vector3 ProcessMotion(Vector3 input);
    public virtual Quaternion ProcessRotation(Vector3 input)
    {
        return transform.rotation;
    }
    #endregion

    #region State Helper
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
    #endregion
}