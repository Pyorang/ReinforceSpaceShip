using Microsoft.Unity.VisualStudio.Editor;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using DG.Tweening;
using TMPro;
using static UnityEngine.Rendering.DebugUI;

public class InGameUIController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lifeCountText;
    [SerializeField] private TextMeshProUGUI attackDamageText;
    [SerializeField] private TextMeshProUGUI bombText;

    public void Start()
    {
        PlayerStatus.uiChanged += ChangePlayerStatusUI;
    }

    private void OnDestroy()
    {
        PlayerStatus.uiChanged -= ChangePlayerStatusUI;
    }

    private void Update()
    {
        HandleInput();
    }

    public void Init() { }

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
        UIManager.Instance.OpenUI<InGameSettingsUI>(uiData);
    }

    public void ChangePlayerStatusUI(int textType, int value)
    {
        switch (textType)
        {
            case 1:
                lifeCountText.text = "X " + value.ToString();
                break;
            case 2:
                attackDamageText.text = "X " + value.ToString();
                break;
            case 3:
                bombText.text = "X " + value.ToString();
                break;
        }
    }

}
