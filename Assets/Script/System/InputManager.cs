using UnityEngine;
using System.Collections;

public static class InputManager
{
    #region Axis
    public static float MainHorizontal()
    {
        // joystick ad (+ws)
        if (Input.GetAxis("j_Main_X") != 0)
            return Input.GetAxis("j_Main_X");

        // keyboard 1st axis stick
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
        // joystick 2nd axis stick
        if (Input.GetAxis("j_Secondary_X") != 0)
            return Input.GetAxis("j_Secondary_X");

        // keyboard arrow keys
        if (Input.GetAxis("k_Secondary_X") != 0)
            return Input.GetAxis("k_Secondary_X");

        // mouse move when right mouse button is clicked
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

    public static bool PlusButton()
    {
        return (Input.GetButtonDown("j_Plus") || Input.GetButtonDown("k_Plus"));
    }

    public static bool MinusButton()
    {
        return (Input.GetButtonDown("j_Minus") || Input.GetButtonDown("k_Minus"));
    }

    public static bool BackButton()
    {
        return (Input.GetButtonDown("j_Back") || Input.GetButtonDown("k_Back"));
    }

    public static bool StartButton()
    {
        return (Input.GetButtonDown("j_Start") || Input.GetButtonDown("k_Start"));
    }
    #endregion
}
