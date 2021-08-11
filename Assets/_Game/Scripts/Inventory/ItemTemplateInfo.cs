using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class ItemTemplateInfo : MonoBehaviour
{
    public delegate void ItemSlotActivated(GameObject gameObject, Button button, Item scriptableObject);
    public static event ItemSlotActivated onItemActivation;

    [SerializeField] private List<AudioClip> listClips;

    [SerializeField] private Image itemSprite;
    [SerializeField] private TMP_Text itemName;

    [SerializeField] private GameObject buttonObject;
    [SerializeField] private Button button;

    private AudioSource audioSource;
    Item scriptable;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayAudio(int index)
    {
        audioSource.volume = DataHolder.Instance.settings.SFX_Volume * 0.1f;
        audioSource.PlayOneShot(listClips[index]);
    }

    public void SetTemplateInfo(Item item)
    {
        scriptable = item;

        itemSprite.sprite = item.Sprite;
        itemName.text = item.Name[DataHolder.Instance.settings.Language_Id];
    }

    public void ActivateButton()
    {
        if (onItemActivation != null)
        {
            onItemActivation(buttonObject, button, scriptable);
        }
    }
}
