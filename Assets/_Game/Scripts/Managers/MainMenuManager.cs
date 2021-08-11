using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
using UnityEngine.Video;
using DG.Tweening;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField] private Image imgFade;
    [SerializeField] private Image imgBackground;
    [SerializeField] private Image imgLogo;

    [SerializeField] private VideoPlayer videoPlayer;
    [SerializeField] private List<VideoClip> clips;

    [SerializeField] private GameObject pnlMainMenu;
    [SerializeField] private GameObject pnlOptions;

    [SerializeField] private List<Button> btnMainMenu;
    [SerializeField] private List<GameObject> btnOptions;

    [SerializeField] private float fadeDuration;
    [SerializeField] private float pauseDuration;
    [SerializeField] private float zoomDuration;
    [SerializeField] private Vector3 zoomVector;
    [SerializeField] private Transform zoomPosition;

    private bool isAnimatingIntro;
    private bool isPlayingCutscene;

    EventSystem current;

    private void Start()
    {
        current = EventSystem.current;

        Time.timeScale = 1;

        print("a");

        imgFade.DOFade(1f, 0f);
        imgLogo.DOFade(0f, 0f);
        pnlMainMenu.transform.DOScale(0f, 0f);
        pnlOptions.transform.DOScale(0f, 0f);
        pnlOptions.SetActive(false);
        pnlOptions.SetActive(false);

        MusicManager.Instance.gameObject.SetActive(true);
        AudioManager.Instance.gameObject.SetActive(true);

        current.gameObject.GetComponent<InputSystemUIInputModule>().enabled = true;

        PlayIntro(current);
    }

    public void PlayIntro(EventSystem eventSystem)
    {
        StartCoroutine(IntroSequence(eventSystem));
    }

    private void Update()
    {
        if (Keyboard.current.zKey.wasPressedThisFrame)
        {
            if (isAnimatingIntro)
                SkipIntro(current);

            if (isPlayingCutscene)
                SceneManager.LoadScene(2);
        }
    }

    private IEnumerator IntroSequence(EventSystem eventSystem)
    {
        isAnimatingIntro = true;
        eventSystem.gameObject.GetComponent<InputSystemUIInputModule>().enabled = false;
        imgBackground.gameObject.SetActive(true);
        imgFade.DOFade(0f, fadeDuration);
        yield return new WaitForSeconds(fadeDuration);

        yield return new WaitForSeconds(pauseDuration * 0.33f);

        imgLogo.DOFade(1f, pauseDuration * 0.33f);
        yield return new WaitForSeconds(pauseDuration * 0.66f);

        imgBackground.transform.DOScale(zoomVector, zoomDuration);
        imgBackground.transform.DOMove(zoomPosition.position, zoomDuration);
        imgLogo.DOFade(0f, zoomDuration * 0.5f);
        yield return new WaitForSeconds(zoomDuration);

        yield return new WaitForSeconds(pauseDuration * 0.25f);


        pnlMainMenu.SetActive(true);
        pnlMainMenu.transform.DOScale(1f, fadeDuration * 0.5f);
        yield return new WaitForSeconds(fadeDuration * 0.5f);

        eventSystem.gameObject.GetComponent<InputSystemUIInputModule>().enabled = true;
        isAnimatingIntro = false;

        eventSystem.SetSelectedGameObject(btnMainMenu[0].gameObject);
    }

    private void SkipIntro(EventSystem eventSystem)
    {
        StopAllCoroutines();

        imgLogo.DOFade(0f, 0f);

        imgBackground.transform.DOScale(zoomVector, 0f);
        imgBackground.transform.DOMove(zoomPosition.position, 0f);

        imgLogo.DOFade(0f, 0f);

        pnlMainMenu.SetActive(true);
        pnlMainMenu.transform.DOScale(1f, 0f);

        eventSystem.gameObject.GetComponent<InputSystemUIInputModule>().enabled = true;
        isAnimatingIntro = false;

        eventSystem.SetSelectedGameObject(btnMainMenu[0].gameObject);
    }

    public void StartGame()
    {
        imgFade.DOFade(1f, fadeDuration).OnComplete(delegate {
            //SceneManager.LoadScene(2); 
            videoPlayer.clip = clips[DataHolder.Instance.settings.Language_Id];
            videoPlayer.Play();
            isPlayingCutscene = true;

            MusicManager.Instance.gameObject.SetActive(false);
            AudioManager.Instance.gameObject.SetActive(false);

            current.gameObject.GetComponent<InputSystemUIInputModule>().enabled = false;
        });
    }

    public void OpenOptions()
    {
        pnlMainMenu.transform.DOScale(0f, fadeDuration * 0.5f).OnComplete(delegate { 
            pnlMainMenu.SetActive(false);
            pnlOptions.SetActive(true);
            pnlOptions.transform.DOScale(1f, fadeDuration * 0.5f).OnComplete(delegate { current.SetSelectedGameObject(btnOptions[0]); });
        });
    }

    public void CloseOptions()
    {
        pnlOptions.transform.DOScale(0f, fadeDuration * 0.5f).OnComplete(delegate {
            pnlOptions.SetActive(false);
            pnlMainMenu.SetActive(true);
            pnlMainMenu.transform.DOScale(1f, fadeDuration * 0.5f).OnComplete(delegate { current.SetSelectedGameObject(btnMainMenu[0].gameObject); });
        });
    }

    public void Credits()
    {
        imgFade.DOFade(1f, fadeDuration).OnComplete(delegate { SceneManager.LoadScene(3); });
    }

    public void Quit()
    {
        imgFade.DOFade(1f, fadeDuration).OnComplete(delegate { Application.Quit(); });
    }

    private void OnEnable()
    {
        videoPlayer.loopPointReached += delegate {
            imgFade.DOFade(1f, 4f).OnComplete(delegate { SceneManager.LoadScene(2); }); 
        };
    }

    private void OnDisable()
    {
        videoPlayer.loopPointReached -= delegate { 
            imgFade.DOFade(1f, 4f).OnComplete(delegate { SceneManager.LoadScene(2); }); 
        };
    }
}
