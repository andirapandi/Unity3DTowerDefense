using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class TowerInfo
{
    public Vector3 Position { get; set; }
    public bool IsOccupied { set; get; }
}

public class BuildMode : MonoBehaviour
{
    public List<GameObject> towerPrefabs;

    List<TowerInfo> towerSpawns;
    Transform playerTransform;
    Transform cameraTransform;
    GameObject spawnPreview;

    bool isActive = false;

    const float PREVIEW_DISTANCE_FROM_PLAYER = 3;

    const float SPAWN_PREVIEW_SNAP_SPHERE_RADIUS = 2;

    void Start()
    {
        towerSpawns = new List<TowerInfo>();
        // Get all the tower info in the map
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("TowerSpawn"))
        {
            towerSpawns.Add(new TowerInfo { Position = go.transform.position, IsOccupied = false });
        }
        // Create the Tower Preview (spawnPreview)
        spawnPreview = Instantiate(towerPrefabs[0]) as GameObject;
        //spawnPreview.active = false; // obsolete warning
        spawnPreview.SetActive(false);
    }

    void Update()
    {
        PoolInput();

        if (!isActive)
            return;

        MoveSpawnPreview();
    }

    private void MoveSpawnPreview()
    {
        if (playerTransform == null || cameraTransform == null)
        {
            playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
            cameraTransform = Camera.main.transform;
            return;
        }
        Vector3 previewPosition = playerTransform.position;
        #region make tower always have same distance from player - independent of angle
        var tempVector = (playerTransform.position - cameraTransform.position);
        tempVector.y = 0;
        #endregion
        previewPosition += tempVector.normalized * PREVIEW_DISTANCE_FROM_PLAYER;
        RaycastHit hit;
        if (Physics.Raycast(previewPosition + Vector3.up * 5, Vector3.down, out hit, 10, LayerMask.GetMask("Ground")))
            previewPosition.y = hit.point.y;
        //actually, we would really have to create 4 raycasts - and also check for collisions :) perhaps make tower object red

        var towerSpawnColliders = Physics.OverlapSphere(previewPosition, SPAWN_PREVIEW_SNAP_SPHERE_RADIUS, LayerMask.GetMask("TowerSpawn"));
        if (towerSpawnColliders.Length > 0)
        {
#if _AWEFIJWAEF_
            float closestDistanceSqr = Vector3.SqrMagnitude(previewPosition - towerSpawnColliders[0].transform.position);
            int closestIndex = 0;
            for (int i = 1; i < towerSpawnColliders.Length; ++i)
            {
                float d = Vector3.SqrMagnitude(previewPosition - towerSpawnColliders[i].transform.position);
                if (d < closestDistanceSqr)
                {
                    closestDistanceSqr = d;
                    closestIndex = i;
                }
            }
            previewPosition = towerSpawns.Find(t => t.Position == towerSpawnColliders[closestIndex].transform.position).Position;
#else
            // thought this was simpler to write, looks a little crazy now and likely is not faster than original solution
            previewPosition = towerSpawnColliders.Select(tsc => tsc.transform.position).Intersect(towerSpawns.Select(ts => ts.Position))
                .Select(pos => new { position = pos, distance = Vector3.SqrMagnitude(previewPosition - pos) })
                .OrderBy(ele => ele.distance).Select(ele => ele.position).First();
#endif
        }

        // .. temporary fix ...
        previewPosition.y += 1;

        spawnPreview.transform.position = previewPosition;
    }

    private void PoolInput()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            if (isActive)
                DisableBuildMode();
            else
                ActivateBuildMode();
        }
    }

    private void ActivateBuildMode()
    {
        isActive = true;
        spawnPreview.SetActive(true);
    }

    private void DisableBuildMode()
    {
        isActive = false;
        spawnPreview.SetActive(false);
    }
}