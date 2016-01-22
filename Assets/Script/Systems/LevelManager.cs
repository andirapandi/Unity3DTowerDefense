using UnityEngine;
using System.Collections;

public class LevelManager : MonoSingleton<LevelManager>
{
    int lifePoint = 10;

    public void EnemyCrossed()
    {
        lifePoint--;
        if (lifePoint == 0)
            Defeat();
    }

    void Defeat()
    {
        // Wipe all the enemies
        // clean the level
        Debug.Log("Defeat");
    }

    void Update()
    {
        // temp test
        if (Input.GetKeyDown(KeyCode.K))
            GetComponent<Wave>().StartWave();
    }
}
