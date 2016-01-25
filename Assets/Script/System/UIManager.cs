using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject root;
    List<GameObject> allUI = new List<GameObject>();

    #region Init & Update
    public override void Init()
    {
        waveInfoText = waveInfo.GetComponentsInChildren<Text>();
        recapInfoText = recapInfo.GetComponentsInChildren<Text>();
        foreach (Transform t in root.transform) // var t does not work here?!!
        {
            allUI.Add(t.gameObject);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameMenu();
        }
    }
    #endregion

    #region Game Menu
    public GameObject gameMenuObject;
    bool gameMenuShowing = false;

    public void ToggleGameMenu()
    {
        gameMenuShowing = !gameMenuShowing;
        foreach (var go in allUI)
        {
            go.SetActive(!gameMenuShowing);
        }
        recapInfo.SetActive(false);
        gameMenuObject.SetActive(gameMenuShowing);
    }

    public void RestartLevel()
    {
        //Application.LoadLevel(Application.loadedLevel);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToHUB()
    {
        //Application.LoadLevel("HUB");
        SceneManager.LoadScene("Hub");
    }

    public void ExitToDesktop()
    {
        Application.Quit();
    }
    #endregion

    #region WaveInformation
    public GameObject waveInfo;
    Text[] waveInfoText;

    public void DrawWaveInfo()
    {
        waveInfoText[0].text = "Current wave: " + LevelManager.Instance.GetWaveInfo();
        waveInfoText[1].text = "Enemies on map: " + SpawnManager.Instance.GetEnemiesLeft();
    }
    #endregion

    #region RecapInformation
    public GameObject recapInfo;
    private Text[] recapInfoText;

    public void PopRecapInfo(bool victory, string[] texts)
    {
        foreach (var go in allUI)
            go.SetActive(false);
        recapInfo.SetActive(true);

        recapInfoText[0].text = victory ? "Victory!" : "Defeat!";
        recapInfoText[1].text = texts[0];
        recapInfoText[2].text = texts[1];
        recapInfoText[3].text = texts[2];
    }
    #endregion
}