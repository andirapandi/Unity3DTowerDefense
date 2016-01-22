using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject root;
    public GameObject waveInfo;
    Text[] waveInfoText;

    public override void Init()
    {
        waveInfoText = waveInfo.GetComponentsInChildren<Text>();
    }

    public void DrawWaveInfo()
    {
        waveInfoText[0].text = "Current wave: " + LevelManager.Instance.GetWaveInfo();
        waveInfoText[1].text = "Enemies on map: " + SpawnManager.Instance.GetEnemiesLeft();
    }
}
