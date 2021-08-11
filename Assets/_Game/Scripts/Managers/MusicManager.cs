using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    #region Singleton
    public static MusicManager Instance;

    private void Awake()
    {
        Instance = this;
    }
    #endregion

    [SerializeField] private List<AudioClip> audioClips = default;
    [SerializeField] private bool isLoopAwake;
    private AudioSource audioSource;

    private bool isMusicPlaying;

    private void Start()
    {
        isMusicPlaying = true;

        audioSource = GetComponent<AudioSource>();

        audioSource.volume = DataHolder.Instance.settings.Music_Volume * 0.1f;

        audioSource.clip = audioClips[0];

        if (isLoopAwake)
        {
            audioSource.Play();
        }
    }

    private void Update()
    {
        if (isLoopAwake)
            return;

        if (!GameManager.Instance.isEnemyActive && isMusicPlaying)
        {
            audioSource.enabled = false;
            isMusicPlaying = false;
        }

        else if (GameManager.Instance.isEnemyActive && !isMusicPlaying)
        {
            audioSource.enabled = true;
            isMusicPlaying = true;
            audioSource.Play();
        }
    }

    public void ChangeVolume(float value)
    {
        audioSource.volume = value * 0.1f;
    }

    public void ChangeSong(AudioClip clip)
    {
        audioSource.clip = clip;
        audioSource.Play();
    }
}
