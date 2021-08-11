using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    public List<Item> itemList;

    public Inventory()
    {
        itemList = new List<Item>();
    }

    public void AddItem(Item item)
    {
        itemList.Add(item);
    }

    public void RemoveItem(Item removeItem)
    {
        List<Item> removing = new List<Item>(itemList.Count);

        for (int i = 0; i < itemList.Count; i++)
        {
            Item currentItem = itemList[i];
            if (currentItem == removeItem)
            {
                removing.Add(currentItem);
                break;
            }
        }

        for (int index = 0; index < removing.Count; index++)
        {
            itemList.Remove(removing[index]);
        }
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }
}
