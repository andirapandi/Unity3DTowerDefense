using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoSingleton<LevelManager>
{
    int lifePoint = 10;
    int currentWave;
    /// <summary>
    /// total waves
    /// amount of waves?!
    /// </summary>
    int ammWave;
    bool spawnActive = false;
    bool waveActive = false;
    List<Wave> waves = new List<Wave>();

    public override void Init()
    {
        foreach (var w in GetComponents<Wave>())
            waves.Add(w);

        currentWave = 0;
        ammWave = waves.Count;

        // this might cause random problems when UIManager is not instantiated yet
        //UIManager.Instance.DrawWaveInfo();
    }

    void Start()
    {
        // Start called after Initialize
        UIManager.Instance.DrawWaveInfo();
    }

    void Update()
    {
        if (!waveActive)
        {
            if (Input.GetKeyDown(KeyCode.K))
                StartWave();
        }
        else
        {
            if (!spawnActive && !GameObject.FindGameObjectWithTag("Enemy"))
            {
                Debug.Log("Wave cleared");
                waveActive = false;
                if (waves.Count == 0)
                    Victory();
            }
        }
    }

    void StartWave()
    {
        if (waves.Count == 0)
        {
            Debug.Log("Problem, no more waves!");
            return;
        }
        else {
            currentWave++;
            Debug.Log("Wave is starting");
            waves[0].StartWave();
            spawnActive = true;
            waveActive = true;
            UIManager.Instance.DrawWaveInfo();
        }
    }

    public void EndWave()
    {
        Debug.Log("Wave is ending");
        Destroy(waves[0]);
        waves.RemoveAt(0);
        spawnActive = false;
    }

    public void EnemyCrossed()
    {
        lifePoint--;
        if (lifePoint == 0)
            Defeat();
    }

    public string GetWaveInfo()
    {
        return currentWave + " / " + ammWave;
    }

    void Victory()
    {
        Debug.Log("Level is cleared");
    }

    void Defeat()
    {
        // Wipe all the enemies
        // clean the level
        Debug.Log("Defeat");
    }
}
