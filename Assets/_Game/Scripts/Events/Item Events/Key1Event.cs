using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key1Event : MonoBehaviour
{
    public delegate void Key1ItemUsed(Item item);
    public static event Key1ItemUsed onKey1Action;

    public Item Item;

    void Start()
    {
        if (onKey1Action != null && Item != null)
        {
            onKey1Action(Item);
        }
    }
}
