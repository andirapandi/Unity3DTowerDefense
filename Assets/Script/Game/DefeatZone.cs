using UnityEngine;
using System.Collections;

public class DefeatZone : MonoBehaviour {

    void OnTriggerEnter(Collider col)
    { 
        if (col.tag == "Enemy")
        {
            LevelManager.Instance.EnemyCrossed();
            SpawnManager.Instance.DestroyEnemy(col.gameObject);
        }
    }
}
