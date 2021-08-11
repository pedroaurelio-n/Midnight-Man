using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerEvent : MonoBehaviour
{
    public delegate void HammerItemUsed(Item item);
    public static event HammerItemUsed onHammerAction;

    public Item Item;

    void Start()
    {
        if (onHammerAction != null && Item != null)
        {
            onHammerAction(Item);
        }
    }
}
