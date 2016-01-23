using UnityEngine;
using System.Collections;

public class BaseProjectile : MonoBehaviour
{
    public Vector3 TargetLocation { get; set; }
    public DamageInfo Damage { get; set; }

    public Transform LockOn { get; set; }
    public bool IsLockedOnTarget { get; set; }
    public float ProjectileSpeed { get; set; }

    bool isLaunched = false;

    public BaseProjectile()
    {
        LockOn = null;
        IsLockedOnTarget = false;
        ProjectileSpeed = 5f;
    }

    void Update()
    {
        if (!isLaunched)
            return;
        if (IsLockedOnTarget && LockOn)
        {
            TargetLocation = LockOn.position;
        }

        transform.position = Vector3.MoveTowards(transform.position, TargetLocation, ProjectileSpeed * Time.deltaTime);
    }

}