using TMPro;
using UnityEngine;

public class InGameSettingsUI : BaseUI
{
    [SerializeField] private GameObject _soundOnToggle;
    [SerializeField] private GameObject _soundOffToggle;

    public override void Init(Transform transform)
    {
        base.Init(transform);
        Time.timeScale = 0f;
    }

    public override void Close(bool isCloseAll = false)
    {
        base.Close(isCloseAll);
        Time.timeScale = 1.0f;
    }

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        var userSettingsData = UserDataManger.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);
        SetSoundSetting(userSettingsData.IsSoundEnable);
    }

    public void OnClickSoundOnToggle()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        var userSettingsData = UserDataManger.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);

        userSettingsData.IsSoundEnable = false;
        UserDataManger.Instance.SaveUserData();
        AudioManager.Instance.Mute();
        SetSoundSetting(userSettingsData.IsSoundEnable);

    }

    public void OnClickSoundOffToggle()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        var userSettingsData = UserDataManger.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);

        userSettingsData.IsSoundEnable = true;
        UserDataManger.Instance.SaveUserData();
        AudioManager.Instance.Unmute();
        SetSoundSetting(userSettingsData.IsSoundEnable);
    }

    private void SetSoundSetting(bool isEnable)
    {
        _soundOnToggle.SetActive(isEnable);
        _soundOffToggle.SetActive(!isEnable);
    }

    public void OnClickGoToLobbyButton()
    {
        Close();
        SceneLoader.Instance.LoadSceneAsync(ESceneType.Lobby);
    }
}
