using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using TMPro;
using UnityEngine.UI;
using UnityEngine.U2D;

public class LobbyUiController : MonoBehaviour
{
    [Header ("Lobby BackGrounds")]
    [Space]
    [SerializeField] private GameObject BlackImage;
    [SerializeField] private GameObject LobbyImage;

    [Header("Lobby UIs")]
    [Space]
    [SerializeField] private TextMeshProUGUI chapterText;
    [SerializeField] private TextMeshProUGUI playText;
    [Space]
    [SerializeField] private Button playButton;
    [Space]
    [SerializeField] private Image planetImage;

    public void Init() 
    {
        BlackImage.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -Screen.height);
        BlackImage.GetComponent<RectTransform>().sizeDelta = new Vector2(Screen.width, Screen.height);
        SetChapter();
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        /*if (Input.GetKeyDown(KeyCode.Escape))
        {
            AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

            var frontUI = UIManager.Instance.GetFrontUI();
            if (frontUI != null)
            {
                frontUI.Close();
            }
            else
            {
                ShowQuitConfirmUI();
            }
        }*/
    }

    /*private void ShowQuitConfirmUI()
    {
        var data = new ConfirmUIData()
        {
            ConfirmType = EConfirmType.OK_CANCEL,
            TitleText = "Quit",
            DescriptionText = "Do you want to quit game?",
            OkButtonText = "Quit",
            CancelButtonText = "Cancel",
            ActionOnClickOkButton = () => Application.Quit()
        };
        UIManager.Instance.OpenUI<ConfirmUI>(data);
    }*/

    public void SetChapter()
    {
        var userChapterData = UserDataManager.Instance.GetUserData<UserChapterData>();
        chapterText.text = $"{userChapterData.CurrentChapterNum}단계";

        SetPlanetImage(userChapterData.CurrentChapterNum);

        CheckChapterAvailable(userChapterData);
    }

    public void SetPlanetImage(int chapterNum)
    {
        string path = "PlanetImage/Planet" + ((chapterNum - 1) / 3 + 1);
        Sprite sprite = Resources.Load<Sprite>(path);

        if (sprite != null)
        {
            planetImage.sprite = sprite;

            RectTransform rectTransform = planetImage.GetComponent<RectTransform>();
            rectTransform.sizeDelta = sprite.rect.size;
        }
        else
        {
            planetImage.sprite = null;
        }
    }

    public void CheckChapterAvailable(UserChapterData userChapterData)
    {
        Object[] stageDataFiles = Resources.LoadAll("StageData");
        int fileCount = stageDataFiles.Length;

        if (userChapterData.CurrentChapterNum > fileCount)
        {
            playButton.interactable = false;
            playText.text = "준비중";
        }
    }

    public void OnClickHowToPlayButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<HowToPlayUI>(uiData);
    }

    public void OnClickSettingsButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        var uiData = new BaseUIData();
        UIManager.Instance.OpenUI<SettingsUI>(uiData);
    }

    public void OnClickPlayButton()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        SceneLoader.Instance.LoadSceneAsync(ESceneType.InGame);
    }

    public void OnClickLobbyImage()
    {
        StartCoroutine(LobbyImageAnimation());
    }

    //고쳐야 하는 부분
    IEnumerator LobbyImageAnimation()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        RectTransform rect = BlackImage.GetComponent<RectTransform>();

        // 1. posY를 0으로 자연스럽게 이동
        yield return rect.DOAnchorPosY(0f, 1f).WaitForCompletion();

        yield return new WaitForSeconds(1f); // 잠깐 대기

        LobbyImage.SetActive(false); // LobbyImage 끄기

        // 2. posY를 Screen.height로 자연스럽게 이동
        yield return rect.DOAnchorPosY(Screen.height, 1f).WaitForCompletion();
    }
}
