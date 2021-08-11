using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorHandleEvent : MonoBehaviour
{
    public delegate void DoorHandleItemUsed(Item item);
    public static event DoorHandleItemUsed onDoorHandleAction;

    public Item Item;

    void Start()
    {
        if (onDoorHandleAction != null && Item != null)
        {
            onDoorHandleAction(Item);
        }
    }
}
