using UnityEngine;
using System.Collections;

public abstract class BaseCameraState : MonoBehaviour
{
    protected CameraMotor motor;

    #region BaseState implementation
    public virtual void Construct()
    {
        motor = GetComponent<CameraMotor>();
    }

    public virtual void Destruct()
    {
        Destroy(this);
    }

    public virtual void Transition()
    {

    }

    public abstract Vector3 ProcessMotion(Vector3 input);
    public virtual Quaternion ProcessRotation(Vector3 input)
    {
        return transform.rotation;
    }
    #endregion

    #region functions
    protected float ClampAngle(float angle, float min, float max)
    {
        // avoid loop
        angle = angle % 360;
        //if (angle < 0)
        //    angle += 360;
        // assert correct logic;
        Debug.Assert(angle > -360);
        Debug.Assert(angle <= 360);
        return Mathf.Clamp(angle, min, max);
        //do
        //{
        //    if (angle < -360)
        //        angle += 360;
        //    if (angle > 360)
        //        angle -= 360;
        //} while (angle < 360 || angle > 360);
    }
    #endregion
}
