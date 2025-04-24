using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;

public class LobbyUiController : MonoBehaviour
{
    [SerializeField] private GameObject BlackImage;
    [SerializeField] private GameObject LobbyImage;

    public void Init() { }

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
