using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    #region Singleton
    public static AudioManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private List<AudioClip> audioClips = default;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();

        audioSource.volume = DataHolder.Instance.settings.SFX_Volume * 0.1f;
    }

    public void PlayAudio(int index)
    {
        audioSource.PlayOneShot(audioClips[index]);
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value * 0.1f;
    }
}
