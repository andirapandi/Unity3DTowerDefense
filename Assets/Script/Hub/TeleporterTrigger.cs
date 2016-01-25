using UnityEngine;
using System.Collections;

public class TeleporterTrigger : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        SendMessageUpwards("EnterLevel");
    }
}