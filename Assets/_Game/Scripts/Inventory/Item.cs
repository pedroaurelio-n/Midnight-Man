using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Item", menuName = "Item")]
public class Item : ScriptableObject
{
    public List<string> Name;
    public List<string> Description;
    public Sprite Sprite;
    public bool isUsable;
    public GameObject UseItem;
}
