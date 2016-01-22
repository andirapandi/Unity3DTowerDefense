using UnityEngine;
using System.Collections;
using System;

public class FirstPersonCamera : BaseCameraState
{
    #region Consts
    const float Y_ANGLE_MIN = -75f;
    const float Y_ANGLE_MAX = 50f;
    #endregion

    #region Fields
    float offset = 1f;
    float currentX = 0f;
    float currentY = 0f;
    float sensitivityX = 5f;
    float sensitivityY = 2f;
    #endregion

    #region BaseState implementation
    public override Vector3 ProcessMotion(Vector3 input)
    {
        return transform.position + transform.up * offset;
    }

    public override Quaternion ProcessRotation(Vector3 input)
    {
        currentX += input.x * sensitivityX;
        currentY += input.z * sensitivityY;

        // Clamp my CurrentY
        currentY = ClampAngle(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);

        return Quaternion.Euler(currentY, currentX, 0);
        //return transform.rotation;
    }
    #endregion
}
