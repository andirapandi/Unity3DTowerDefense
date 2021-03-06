﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;

public class TowerInfo
{
    public GameObject TowerObject { get; set; }
    public BaseTower Tower { set; get; }
    public Vector3 Position { get; set; }
    public bool IsOccupied { set; get; }
}

public class BuildMode : MonoBehaviour
{
    const float PREVIEW_DISTANCE_FROM_PLAYER = 3;
    const float SPAWN_PREVIEW_SNAP_SPHERE_RADIUS = 2;

    public List<GameObject> towerPrefabs;

    int resource = 200;
    int selectedTowerIndex = 0;
    TowerInfo focusedTowerSpawn;
    List<TowerInfo> towerSpawns;
    Transform playerTransform;
    Transform cameraTransform;
    GameObject spawnPreview;

    bool isActive = false;


    void Start()
    {
        towerSpawns = new List<TowerInfo>();
        // Get all the tower info in the map
        foreach (GameObject go in GameObject.FindGameObjectsWithTag("TowerSpawn"))
        {
            towerSpawns.Add(new TowerInfo { Position = go.transform.position, IsOccupied = false });
        }
        OnSelectionChanged();
        // fix for preview being displayed already at start of game
        spawnPreview.SetActive(false);

        UIManager.Instance.DrawBuildResource(resource);
    }

    void Update()
    {
        PoolInput();

        if (!isActive)
            return;

        MoveSpawnPreview();
    }

    void SpawnTower()
    {
        if (focusedTowerSpawn == null)
            return;
        if (focusedTowerSpawn.IsOccupied == true)
        {
            UIManager.Instance.ShowGeneralMessage("This tower spawn position is occupied...", Color.red, 2f);
            return;
        }

        // If enough Mana/Resources
        int cost = 50;
        if (resource >= cost)
        {
            resource -= cost;
            UIManager.Instance.DrawBuildResource(resource);
            // temp fix: + Vector3.up (use proper pivot point later on)
            focusedTowerSpawn.TowerObject = Instantiate(towerPrefabs[selectedTowerIndex], focusedTowerSpawn.Position + Vector3.up, Quaternion.identity) as GameObject;
            focusedTowerSpawn.Tower = focusedTowerSpawn.TowerObject.GetComponent<BaseTower>();
            focusedTowerSpawn.IsOccupied = true;

        }
        else
            UIManager.Instance.ShowGeneralMessage("Not enough build resources...", Color.red, 2f);
    }

    private void OnSelectionChanged()
    {
        if (spawnPreview != null)
            Destroy(spawnPreview);
        // Create the Tower Preview (spawnPreview)
        spawnPreview = Instantiate(towerPrefabs[selectedTowerIndex]) as GameObject;
        spawnPreview.GetComponent<BaseTower>().enabled = false;
        spawnPreview.GetComponent<Collider>().enabled = false;
        //spawnPreview.active = false; // obsolete warning
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
#if !_AWEFIJWAEF_
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
            focusedTowerSpawn = towerSpawns.Find(t => t.Position == towerSpawnColliders[closestIndex].transform.position);
            previewPosition = focusedTowerSpawn.Position;
#else
            // thought this was simpler to write, looks a little crazy now and likely is not faster than original solution
            previewPosition = towerSpawnColliders.Select(tsc => tsc.transform.position).Intersect(towerSpawns.Select(ts => ts.Position))
                .Select(pos => new { position = pos, distance = Vector3.SqrMagnitude(previewPosition - pos) })
                .OrderBy(ele => ele.distance).Select(ele => ele.position).First();
#endif
        }
        else
        {
            focusedTowerSpawn = null;
        }

        // .. temporary fix ...
        previewPosition.y += 1;

        spawnPreview.transform.position = previewPosition;
    }

    private void PoolInput()
    {
        // Activate / Deactivate Build Mode
        if (InputManager.AbilityButton())
        {
            if (isActive)
                DisableBuildMode();
            else
                ActivateBuildMode();
        }
        // Change Tower Selection
        if (isActive)
        {
            if (InputManager.MinusButton())
            {
                if (selectedTowerIndex > 0)
                {
                    selectedTowerIndex--;
                    OnSelectionChanged();
                }
            }
            if (InputManager.PlusButton())
            {
                if (selectedTowerIndex < towerPrefabs.Count - 1)
                {
                    selectedTowerIndex++;
                    OnSelectionChanged();
                }
            }
        }
        // Spawn the tower
        if (isActive)
        {
            if (InputManager.InteractButton())
            {
                SpawnTower();
            }
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