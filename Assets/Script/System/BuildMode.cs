using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class BuildMode : MonoBehaviour
{
    public List<GameObject> towerPrefabs;

    Transform playerTransform;
    Transform cameraTransform;
    GameObject spawnPreview;

    bool isActive = false;

    const float PREVIEW_DISTANCE_FROM_PLAYER = 3;

    void Start()
    {
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
        previewPosition += (playerTransform.position - cameraTransform.position).normalized * PREVIEW_DISTANCE_FROM_PLAYER;
        RaycastHit hit;
        if (Physics.Raycast(previewPosition + Vector3.up * 5, Vector3.down, out hit, 10, LayerMask.GetMask("Ground")))
            previewPosition.y = hit.point.y + 1;

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