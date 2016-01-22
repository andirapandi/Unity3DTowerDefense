using UnityEngine;
using System.Collections;

public class CameraMotor : MonoBehaviour
{
    #region Fields
    BaseCameraState state;
    public Transform CameraContainer { set; get; }
    public Vector3 InputVector { get; set; }
    #endregion

    #region Init & Update
    public void Init()
    {
        CameraContainer = new GameObject("Camera Container").transform;
        CameraContainer.gameObject.AddComponent<Camera>();
        state = gameObject.AddComponent<ThirdPersonCamera>() as BaseCameraState;
        state.Construct();
    }

    public void Update()
    {
        var dir = Vector3.zero;

        // get values from newly created inputs, mapped to mouse right now
        dir.x = Input.GetAxis("Horizontal2");
        //dir.x = Input.GetAxis("Mouse X");
        dir.z = -Input.GetAxis("Vertical2");

        if (dir.magnitude > 1)
            dir.Normalize();

        InputVector = dir;
    }

    void LateUpdate()
    {
        CameraContainer.position = state.ProcessMotion(InputVector);
        CameraContainer.rotation = state.ProcessRotation(InputVector);
    }
    #endregion

    #region Functions
    public void ChangeState(string stateName)
    {
        System.Type t = System.Type.GetType(stateName);

        state.Destruct();
        state = gameObject.AddComponent(t) as BaseCameraState;
        state.Construct();
    }
    #endregion
}
