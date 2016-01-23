using UnityEngine;
using System.Collections;

public class ArrowProjectile : BaseProjectile
{
    public override void Launch(Transform tower, Transform target, DamageInfo dmg)
    {
        base.Launch(tower, target, dmg);
        TimeToTarget = .5f;
        IsLockedOnTarget = true;
    }

    protected override void Update()
    {
        base.Update();

        transform.LookAt(TargetLocation);
    }
}