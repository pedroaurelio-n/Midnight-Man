using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AtticKeyEvent : MonoBehaviour
{
    public delegate void AtticKeyItemUsed(Item item);
    public static event AtticKeyItemUsed onAtticKeyAction;

    public Item Item;

    void Start()
    {
        if (onAtticKeyAction != null && Item != null)
        {
            onAtticKeyAction(Item);
        }
    }
}
