using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainmenuOptions : MonoBehaviour
{
    [SerializeField] private Slider sliderSFX;
    [SerializeField] private Slider sliderMusic;

    private void Start()
    {
        sliderMusic.value = DataHolder.Instance.settings.Music_Volume;
        sliderSFX.value = DataHolder.Instance.settings.SFX_Volume;
    }

    private void Update()
    {
        sliderMusic.onValueChanged.AddListener(delegate {
            DataHolder.Instance.settings.ChangeMusicVolume(sliderMusic.value);
            MusicManager.Instance.ChangeVolume(DataHolder.Instance.settings.Music_Volume); 
        });

        sliderSFX.onValueChanged.AddListener(delegate { 
            DataHolder.Instance.settings.ChangeSFXVolume(sliderSFX.value);
            AudioManager.Instance.ChangeVolume(DataHolder.Instance.settings.SFX_Volume); 
        });
    }
}
