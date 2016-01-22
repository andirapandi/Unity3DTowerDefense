using UnityEngine;
using System.Collections;

public class MeleeAttackSpell : BaseSpell
{
    float hitLength;
    Transform hitOrigin;
    LayerMask targetMask;

    public MeleeAttackSpell()
    {
        cooldown = 0.5f;
        lastCast = 0; // should be the same for initialization and allowing first cast straight away - and throws no exception Time.time - cooldown;
        hitLength = 3f;
        hitOrigin = transform;
        targetMask = LayerMask.GetMask("Enemy");
    }

    public override void Action()
    {
        var dmg = new DamageInfo();
        dmg.amount = 5;

        foreach (Collider c in Physics.OverlapSphere(hitOrigin.position, hitLength, targetMask))
        {
            c.SendMessage("OnDamage", dmg);
        }
    }
}
