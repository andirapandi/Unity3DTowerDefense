using UnityEngine;
using System.Collections;
using System;

public class BaseTower : MonoBehaviour
{
    protected float lastTick;
    protected float refreshRate = 0.1f;

    protected float lastAction;
    protected float cooldown = 1f;

    protected float range = 5f;

    void Update()
    {
        if (Time.time - lastAction > cooldown)
        {
            // Refresh every 0.10f to find a Target
            // performance optimisation..
            if (Time.time - lastTick > refreshRate)
            {
                lastTick = Time.time;
                // Get a Target
                Transform target = GetNearestEnemy();
                if (target != null)
                {
                    Action(target);
                }
            }
        }
    }

    private Transform GetNearestEnemy()
    {
        // Test if there ar eany enemies within our range
        Collider[] allEnemies = Physics.OverlapSphere(transform.position, range, LayerMask.GetMask("Enemy"));

        if (allEnemies.Length != 0)
        {
            int closestIndex = 0;
            float nearestDistance = Vector3.SqrMagnitude(transform.position - allEnemies[0].transform.position);

            for (int i = 1; i < allEnemies.Length; ++i)
            {
                float distance = Vector3.SqrMagnitude(transform.position - allEnemies[i].transform.position);
                if (distance < nearestDistance)
                {
                    nearestDistance = distance;
                    closestIndex = i;
                }
            }
            return allEnemies[closestIndex].transform;
        }
        return null;
    }

    private void Action(Transform target)
    {
        lastAction = Time.time;
        Debug.Log(gameObject.name + " is shooting at " + target.name);
        Debug.DrawRay(transform.position, target.position - transform.position, Color.red, 1.5f);
    }
}