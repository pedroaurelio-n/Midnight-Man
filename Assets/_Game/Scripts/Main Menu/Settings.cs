using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Settings", menuName = "Settings")]
public class Settings : ScriptableObject
{
    public int Language_Id;
    public float SFX_Volume;
    public float Music_Volume;

    public void ChangeLanguage(int id)
    {
        Language_Id = id;
    }

    public void ChangeSFXVolume(float value)
    {
        SFX_Volume = value;
    }

    public void ChangeMusicVolume(float value)
    {
        Music_Volume = value;
    }
}
