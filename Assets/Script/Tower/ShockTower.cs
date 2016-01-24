using UnityEngine;
using System.Collections;

public class ShockTower : BaseTower
{
    public ShockTower()
    {
        range = 5f;
        cooldown = 1f;
    }

    protected override void Action(Transform target)
    {
        Debug.DrawRay(transform.position, target.position - transform.position, Color.red, 1.5f);
     
        //base.Action(target);
        lastAction = Time.time;

        DamageInfo dmg = new DamageInfo();
        dmg.amount = 2;

        foreach (var c in Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy")))
        {
            //c.GetComponent<AICombat>().OnDamage(dmg);
            c.SendMessage("OnDamage", dmg);
        }
    }

    //// Use this for initialization
    //void Start()
    //{

    //}

    //// Update is called once per frame
    //void Update()
    //{

    //}
}