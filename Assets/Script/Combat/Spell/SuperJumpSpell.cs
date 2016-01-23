using UnityEngine;
using System.Collections;

public class SuperJumpSpell : BaseSpell
{
    private BaseMotor motor;

    public SuperJumpSpell()
    {
        cooldown = 3f;
        lastCast = float.MinValue; // 0; // should be the same for initialization and allowing first cast straight away - and throws no exception Time.time - cooldown;
        motor = GetComponent<BaseMotor>();
    }

    public override void Action()
    {
        motor.ChangeState("SuperJumpState");
    }
}
