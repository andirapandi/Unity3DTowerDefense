using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class LevelManager : MonoSingleton<LevelManager>
{
    #region Fields
    int hitPoint = maxHitpoint;
    const int maxHitpoint = 10;
    int currentWave;
    /// <summary>
    /// total waves
    /// amount of waves?!
    /// </summary>
    int ammWave;
    bool spawnActive = false;
    bool waveActive = false;
    List<Wave> waves = new List<Wave>();
    #endregion

    #region Init & Update
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
        UIManager.Instance.DrawHitpoint(hitPoint, maxHitpoint);
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
    #endregion

    #region Wave Functions
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

    public string GetWaveInfo()
    {
        return currentWave + " / " + ammWave;
    }
    #endregion

    #region Functions
    public void EnemyCrossed()
    {
        hitPoint--;
        UIManager.Instance.DrawHitpoint(hitPoint, maxHitpoint);
        if (hitPoint == 0)
            Defeat();
    }

    void Victory()
    {
        Debug.Log("Level is cleared");

        string[] texts = new string[3];
        texts[0] = "Total Damage: x";
        texts[1] = "Towers built: x";
        texts[2] = "Good Job!";

        UIManager.Instance.PopRecapInfo(true, texts);
    }

    void Defeat()
    {
        // Wipe all the enemies
        // clean the level
        Debug.Log("Defeat");

        string[] texts = new string[3];
        texts[0] = "Total Damage: x";
        texts[1] = "Towers built: x";
        texts[2] = "Try again!";

        UIManager.Instance.PopRecapInfo(false, texts);
    }
    #endregion
}