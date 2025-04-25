using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using TMPro;

public class LobbyUiController : MonoBehaviour
{
    [Header ("Lobby BackGrounds")]
    [Space]
    [SerializeField] private GameObject BlackImage;
    [SerializeField] private GameObject LobbyImage;

    [Header("Lobby UIs")]
    [Space]
    [SerializeField] private TextMeshProUGUI chapterText;

    public void Init() 
    {
        SetChapterText();
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

    public void SetChapterText()
    {
        var userChapterData = UserDataManager.Instance.GetUserData<UserChapterData>();
        Debug.Assert(userChapterData != null);
        chapterText.text = $"{userChapterData.CurrentChapterNum}´Ü°è";
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

    IEnumerator LobbyImageAnimation()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");
        BlackImage.GetComponent<RectTransform>().DOAnchorPosY(0f, 1f);
        yield return new WaitForSeconds(2f);

        LobbyImage.SetActive(false);

        BlackImage.GetComponent<RectTransform>().DOAnchorPosY(1920f, 1f);
        yield return null;
    }
}
