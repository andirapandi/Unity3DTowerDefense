using UnityEngine;
using System.Collections;

public static class InputManager
{
    #region Axis
    public static float MainHorizontal()
    {
        if (Input.GetAxis("j_Main_X") != 0)
            return Input.GetAxis("j_Main_X");

        if (Input.GetAxis("k_Main_X") != 0)
            return Input.GetAxis("k_Main_X");

        return 0;
    }

    public static float MainVertical()
    {
        if (Input.GetAxis("j_Main_Y") != 0)
            return Input.GetAxis("j_Main_Y");

        return Input.GetAxis("k_Main_Y");
    }

    public static float SecondaryHorizontal()
    {
        if (Input.GetAxis("j_Secondary_X") != 0)
            return Input.GetAxis("j_Secondary_X");

        if (Input.GetAxis("k_Secondary_X") != 0)
            return Input.GetAxis("k_Secondary_X");

        if (Input.GetMouseButton(1))
            if (Input.GetAxis("Horizontal2") != 0)
                return Input.GetAxis("Horizontal2");

        return 0;
    }

    public static float SecondaryVertical()
    {
        if (Input.GetAxis("j_Secondary_Y") != 0)
            return Input.GetAxis("j_Secondary_Y");

        if (Input.GetAxis("k_Secondary_Y") != 0)
            return Input.GetAxis("k_Secondary_Y");

        if (Input.GetMouseButton(1))
            if (Input.GetAxis("Vertical2") != 0)
                return Input.GetAxis("Vertical2");

        return 0;
    }
    #endregion

    #region Buttons

    public static bool ActionButton()
    {
        return (Input.GetButtonDown("j_Action") || Input.GetButtonDown("k_Action"));
    }

    public static bool InteractButton()
    {
        return (Input.GetButtonDown("j_Interact") || Input.GetButtonDown("k_Interact"));
    }

    public static bool CancelButton()
    {
        return (Input.GetButtonDown("j_Cancel") || Input.GetButtonDown("k_Cancel"));
    }

    public static bool AbilityButton()
    {
        return (Input.GetButtonDown("j_Ability") || Input.GetButtonDown("k_Ability"));
    }
    #endregion
}
