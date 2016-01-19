using UnityEngine;
using System.Collections;

public class CameraMotor : MonoBehaviour
{

    BaseCameraState state;
    public Transform CameraContainer { set; get; }
    public Vector3 InputVector { get; set; }
    public void Init()
    {
        CameraContainer = new GameObject("Camera Container").transform;
        CameraContainer.gameObject.AddComponent<Camera>();
        state = gameObject.AddComponent<FirstPersonCamera>() as BaseCameraState;
        state.Construct();
    }

    public void Update()
    {
        var dir = Vector3.zero;

        dir.x = Input.GetAxis("Horizontal");
        dir.z = Input.GetAxis("Vertical");

        if (dir.magnitude > 1)
            dir.Normalize();

        InputVector = dir;
    }

    void LateUpdate()
    {
        CameraContainer.position = state.ProcessMotion(InputVector);
        CameraContainer.rotation = state.ProcessRotation(InputVector);
    }

    public void ChangeState(string stateName)
    {
        System.Type t = System.Type.GetType(stateName);

        state.Destruct();
        state = gameObject.AddComponent(t) as BaseCameraState;
        state.Construct();
    }
}
