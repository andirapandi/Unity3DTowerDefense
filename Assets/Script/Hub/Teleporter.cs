using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class Teleporter : MonoBehaviour
{
    public string levelName;
    public GameObject UIObject;

    // Use this for initialization
    void Start()
    {
        UIObject.SetActive(false);
    }

    void OnTriggerEnter(Collider other)
    {
        UIObject.SetActive(true);
    }

    void OnTriggerExit(Collider other)
    {
        UIObject.SetActive(false);
    }

    bool Requirement()
    {
        return true;
    }

    void EnterLevel()
    {
        if (!Requirement())
            return;

        SceneManager.LoadScene(levelName);
    }
}