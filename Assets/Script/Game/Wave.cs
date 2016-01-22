using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Wave : MonoBehaviour
{
    #region Fields
    public List<WaveEvent> events = new List<WaveEvent>();
    bool isPlaying = false;
    #endregion

    #region Initialize & Update
    public void StartWave()
    {
        isPlaying = true;
        if (events.Count != 0)
            events[0].StartEvent();
        else
        {
            LevelManager.Instance.EndWave();
            //Debug.Log("End Wave in StartWave");
        }
    }

    void Update()
    {
        if (!isPlaying)
            return;

        if (!events[0].RunEvent())
        {
            Debug.Log("End Event");
            events.RemoveAt(0);
            if (events.Count == 0)
            {
                LevelManager.Instance.EndWave();
                //Debug.Log("End Wave");
            }
            else
                events[0].StartEvent();
        }
    }
    #endregion


    [System.Serializable]
    public class WaveEvent
    {
        #region Functions
        public float duration = 15f;
        public List<SpawnInfo> spawnInfos;

        float startTime;
        #endregion

        #region Init & Update
        public void StartEvent()
        {
            startTime = Time.time;
        }

        public bool RunEvent()
        {
            if (duration == 0f && spawnInfos.Count == 0)
                // nothing more to do
                return false;
            else if (Time.time - startTime > duration && duration != 0f)
                // time up
                return false;

            for (int i = 0; i < spawnInfos.Count; ++i)
            {
                spawnInfos[i].ReadyToSpawn();
                if (spawnInfos[i].amount == 0)
                    spawnInfos.RemoveAt(i);
            }

            return true;
        }
        #endregion

        [System.Serializable]
        public class SpawnInfo
        {
            #region Fields
            public int spawnPointIndex = 0;
            public int spawnPrefabIndex = 0;
            public int amount = 10;
            public float interval = 1f;

            float lastTime;
            #endregion

            #region Functions
            public void ReadyToSpawn()
            {
                if (Time.time - lastTime >= interval)
                {
                    SpawnManager.Instance.Spawn(spawnPrefabIndex, spawnPointIndex);
                    amount--;
                    lastTime = Time.time;
                }
            }
            #endregion
        }
    }
}
