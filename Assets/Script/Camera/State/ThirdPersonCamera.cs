using UnityEngine;
using System.Collections;
using System;

public class ThirdPersonCamera : BaseCameraState
{
    const float Y_ANGLE_MIN = -75f;
    const float Y_ANGLE_MAX = 50f;

    Transform lookAt;
    Transform cameraContainer;

    Vector3 offset = Vector3.up;
    float distance = 10.0f;
    float currentX = 0f;
    float currentY = 0f;
    float sensitivityX = 4f;
    float sensitivityY = 1f;

    public override void Construct()
    {
        base.Construct();

        lookAt = transform;
        cameraContainer = motor.CameraContainer;
    }

    public override Vector3 ProcessMotion(Vector3 input)
    {
        currentX += input.x * sensitivityX;
        currentY += input.z * sensitivityY;

        // Clamp my CurrentY
        currentY = ClampAngle(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        return CalculatePosition();
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {
        cameraContainer.LookAt(lookAt.position + offset);
        return cameraContainer.rotation;
    }

    Vector3 CalculatePosition()
    {
        Vector3 dir = new Vector3(0, 0, -distance);
        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);
        return (lookAt.position + offset) + rotation * dir;
    }
}
