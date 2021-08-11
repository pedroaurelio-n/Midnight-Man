using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GearsEvent : MonoBehaviour
{
    public delegate void GearsItemUsed(Item item);
    public static event GearsItemUsed onGearsAction;

    public Item Item;

    void Start()
    {
        if (onGearsAction != null && Item != null)
        {
            onGearsAction(Item);
        }
    }
}
