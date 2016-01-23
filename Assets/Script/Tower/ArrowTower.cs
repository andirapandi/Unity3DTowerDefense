using UnityEngine;
using System.Collections;

public class ArrowTower : BaseTower
{
    public ArrowTower()
    {
        range = 7.5f;
        cooldown = 0.75f;
    }

    protected override void Action(Transform target)
    {
        //base.Action(target);
        lastAction = Time.time;

        DamageInfo dmg = new DamageInfo();
        dmg.amount = 5;

        //target.SendMessage("OnDamage", dmg);
    }
}