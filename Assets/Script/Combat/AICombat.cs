using UnityEngine;
using System.Collections;

public class AICombat : BaseCombat
{
    public override void OnDeath()
    {
        SpawnManager.Instance.DestroyEnemy(this.gameObject);
    }
}
