using UnityEngine;
using System.Collections;

public class Waypoint : MonoBehaviour
{
    public Transform Destination;

    void OnTriggerEnter(Collider col)
    {
        col.SendMessage("SetDestination", Destination);
    }
}
