using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lever1Event : MonoBehaviour
{
    public delegate void Lever1EventStarted();
    public static event Lever1EventStarted onLever1Event;

    void Start()
    {
        if (onLever1Event != null)
        {
            onLever1Event();
        }
    }
}
