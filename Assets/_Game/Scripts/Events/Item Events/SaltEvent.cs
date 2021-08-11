using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaltEvent : MonoBehaviour
{
    public delegate void SaltItemUsed(Item item);
    public static event SaltItemUsed onSaltAction;

    public Item Item;

    void Start()
    {
        if (onSaltAction != null && Item != null)
        {
            onSaltAction(Item);
        }
    }
}
