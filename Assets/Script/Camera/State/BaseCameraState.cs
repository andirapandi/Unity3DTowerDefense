using UnityEngine;
using System.Collections;

public abstract class BaseCameraState : MonoBehaviour
{
    protected CameraMotor motor;

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
}
