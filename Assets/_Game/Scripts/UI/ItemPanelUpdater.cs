using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemPanelUpdater : MonoBehaviour
{
    [SerializeField] private Image itemImage;
    [SerializeField] private TMP_Text itemDescription;
    [SerializeField] private Button useButton;

    Item item;

    public void UpdateInfo(Item item)
    {
        this.item = item;

        itemImage.sprite = item.Sprite;
        itemDescription.text = item.Description[DataHolder.Instance.settings.Language_Id];

        EventSystem.current.SetSelectedGameObject(useButton.gameObject);
    }

    public void UseItem()
    {
        if (!GameManager.Instance.isTextboxOpen)
        {
            GameObject temp = Instantiate(item.UseItem);
            Destroy(temp, 1f);
        }
    }
}
