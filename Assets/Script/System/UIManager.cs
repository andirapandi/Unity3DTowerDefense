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
        resourceInfoText = resourceInfo.GetComponentsInChildren<Text>();
        foreach (Transform t in root.transform) // var t does not work here?!!
        {
            allUI.Add(t.gameObject);
        }
        generalMessages = new List<GeneralMessage>();
        foreach (Transform t in generalMessageContainer.transform)
        {
            var gm = new GeneralMessage();
            gm.go = t.gameObject;
            gm.txt = t.GetComponentInChildren<Text>();
            #region debugging why GetComponentInChildern<Text> return null - looks like this was a runtime problem, after a crash of unity, the problem was gone
            //if (gm.txt == null)
            //    gm.txt = t.GetChild(0).GetComponent<Text>();
            //Debug.Log("component: " + t.name);
            //var allComponents = t.GetComponents(typeof(Component));
            //foreach (var component in allComponents)
            //{
            //    Debug.Log(component.name + " " + component.GetType().Name);
            //}
            //allComponents = t.GetComponentsInChildren(typeof(Component));
            //foreach (var component in allComponents)
            //{
            //    Debug.Log(component.name + " " + component.GetType().Name);
            //}
            //Debug.Log("children: " + t.GetChildCount());
            //Debug.Log("child name: " + t.GetChild(0).name);

            //var message = t.GetChild(0);
            //allComponents = message.GetComponents(typeof(Component));
            //foreach (var component in allComponents)
            //{
            //    Debug.Log(component.name + " in message " + component.GetType().Name);
            //}
            #endregion

            generalMessages.Add(gm);
        }
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ToggleGameMenu();
        }

        foreach (var gm in generalMessages)
            gm.UpdateGeneralMessage();
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

    #region GeneralMessages
    public GameObject generalMessageContainer;
    List<GeneralMessage> generalMessages;

    public void ShowGeneralMessage(string msg, Color color, float duration)
    {
        lock (generalMessageContainer)
        {
            GeneralMessage gm = generalMessages.Find(m => m.go == generalMessageContainer.transform.GetChild(2).gameObject);
            gm.txt.text = msg;
            gm.txt.color = color;
            gm.duration = duration;
            gm.lastShown = Time.time;
            gm.isActive = true;
            gm.go.SetActive(true);
            gm.go.transform.SetAsFirstSibling();
        }
    }

    class GeneralMessage
    {
        public bool isActive = false;
        public GameObject go;
        public Text txt;
        public float duration;
        public float lastShown;

        public void UpdateGeneralMessage()
        {
            if (!isActive)
                return;

            if (Time.time - lastShown > duration)
            {
                isActive = false;
                go.SetActive(false);
            }
        }
    }
    #endregion

    #region ResourceInformation
    public GameObject resourceInfo;
    Text[] resourceInfoText;

    public void DrawBuildResource(int amount)
    {
        resourceInfoText[0].text = "Build resources : " + amount.ToString();
    }

    public void DrawHitpoint(int hitpoint, int maxHitpoint)
    {
        resourceInfoText[1].text = "Hitpoint : " + hitpoint.ToString() + "/" + maxHitpoint.ToString();
    }
    #endregion
}