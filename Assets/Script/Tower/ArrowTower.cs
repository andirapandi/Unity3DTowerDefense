using UnityEngine;
using System.Collections;

public class ArrowTower : BaseTower
{
    public GameObject projectile;

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

        var arrow = Instantiate(projectile, transform.position, Quaternion.identity) as GameObject;
        arrow.GetComponent<BaseProjectile>().Launch(transform, target, dmg);
    }
}