using UnityEngine;
using System.Collections;
using System;

public class BaseProjectile : MonoBehaviour
{
    public Transform Tower { set; get; }
    public Transform Target { set; get; }
    public Vector3 TargetLocation { get; set; }
    public DamageInfo Damage { get; set; }

    //public Transform LockOn { get; set; }
    public bool IsLockedOnTarget { get; set; }
    public float TimeToTarget { get; set; }

    float transition = 0f;
    bool isLaunched = false;

    public BaseProjectile()
    {
        IsLockedOnTarget = false;
        TimeToTarget = 5f;
    }

    protected virtual void Update()
    {
        if (!isLaunched)
            return;

        transition += Time.deltaTime / TimeToTarget;

        if (transition >= 1f)
            ReachTarget();

        if (IsLockedOnTarget && Target)
        {
            TargetLocation = Target.position;
        }

        transform.position = Vector3.Lerp(Tower.position, TargetLocation, transition); // Vector3.MoveTowards(transform.position, TargetLocation, ProjectileSpeed * Time.deltaTime);
    }

    protected virtual void ReachTarget()
    {
        if (Target)
            Target.SendMessage("OnDamage", Damage);
        Destroy(gameObject);
    }

    public virtual void Launch(Transform tower, Transform target, DamageInfo dmg)
    {
        isLaunched = true;
        Tower = tower;
        Target = target;
        TargetLocation = target.position;
        Damage = dmg;
    }
}