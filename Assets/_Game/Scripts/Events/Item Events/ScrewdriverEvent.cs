using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrewdriverEvent : MonoBehaviour
{
    public delegate void ScrewdriverItemUsed(Item item);
    public static event ScrewdriverItemUsed onScrewdriverAction;

    public Item Item;

    void Start()
    {
        if (onScrewdriverAction != null && Item != null)
        {
            onScrewdriverAction(Item);
        }
    }
}
