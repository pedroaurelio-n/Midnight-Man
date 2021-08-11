using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public Inventory inventory;
    [SerializeField] private Transform itemSlotContainer;
    [SerializeField] private GameObject itemSlotTemplate;

    private List<GameObject> itemSlotList;

    private void Awake()
    {
    }

    private void Start()
    {
        print("item count: " + inventory.itemList.Count);
    }

    public void SetInventory(Inventory inventory)
    {
        this.inventory = inventory;
        RefreshInventoryList();
    }

    public void AddItem(Item item)
    {
        inventory.AddItem(item);
        RefreshInventoryList();
        print("item count: " + inventory.itemList.Count);
    }

    public void RemoveItem(Item item)
    {
        inventory.RemoveItem(item);
        RefreshInventoryList();
        print("item count: " + inventory.itemList.Count);
    }

    public void RefreshInventoryList()
    {
        foreach (Transform child in itemSlotContainer)
        {
            if (child == itemSlotTemplate) continue;
            Destroy(child.gameObject);
        }

        itemSlotList = new List<GameObject>();

        foreach (Item item in inventory.GetItemList())
        {
            var itemSlotNewObject = Instantiate(itemSlotTemplate, itemSlotContainer);
            itemSlotNewObject.GetComponent<ItemTemplateInfo>().SetTemplateInfo(item);
            itemSlotList.Add(itemSlotNewObject);
        }
    }

    public List<GameObject> GetInventoryButtonList()
    {
        return itemSlotList;
    }

    private void OnEnable()
    {
        InteractableObject.onItemAdded += AddItem;
    }

    private void OnDisable()
    {
        InteractableObject.onItemAdded -= AddItem;
    }
}
