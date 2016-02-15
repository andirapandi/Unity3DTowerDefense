using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class SpawnPoint
{
    public Transform self;
    public Transform destination;
}

public class SpawnManager : MonoSingleton<SpawnManager>
{
    #region Fields
    public List<SpawnPoint> spawnPoint = new List<SpawnPoint>();
    public List<GameObject> spawnPrefabs = new List<GameObject>();

    List<GameObject> activeEnemies = new List<GameObject>();
    #endregion

    #region Spawn Functions
    public void Spawn(int spawnPrefabIndex)
    {
        Spawn(spawnPrefabIndex, 0);
    }

    public void Spawn(int spawnPrefabIndex, int spawnPointIndex)
    {
        GameObject go = Instantiate(spawnPrefabs[spawnPrefabIndex], spawnPoint[spawnPointIndex].self.position, spawnPoint[spawnPointIndex].self.rotation) as GameObject;
        go.SendMessage("SetDestination", spawnPoint[spawnPointIndex].destination);
        activeEnemies.Add(go);
        UIManager.Instance.DrawWaveInfo();
    }

    public void DestroyEnemy(GameObject go)
    {
        activeEnemies.Remove(go);
        Destroy(go);
        UIManager.Instance.DrawWaveInfo();
    }
    #endregion

    #region Functions
    public int GetEnemiesLeft()
    {
        return activeEnemies.Count;
    }
    #endregion
}
