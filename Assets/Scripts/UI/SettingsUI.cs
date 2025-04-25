using TMPro;
using UnityEngine;

public class SettingsUI : BaseUI
{
    [SerializeField] private GameObject _soundOnToggle;
    [SerializeField] private GameObject _soundOffToggle;

    public override void SetData(BaseUIData data)
    {
        base.SetData(data);

        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);
        SetSoundSetting(userSettingsData.IsSoundEnable);
    }

    public void OnClickSoundOnToggle()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);

        userSettingsData.IsSoundEnable = false;
        UserDataManager.Instance.SaveUserData();
        AudioManager.Instance.Mute();
        SetSoundSetting(userSettingsData.IsSoundEnable);

    }

    public void OnClickSoundOffToggle()
    {
        AudioManager.Instance.Play(AudioType.SFX, "ui_button_click");

        var userSettingsData = UserDataManager.Instance.GetUserData<UserSettingsData>();
        Debug.Assert(userSettingsData != null);

        userSettingsData.IsSoundEnable = true;
        UserDataManager.Instance.SaveUserData();
        AudioManager.Instance.Unmute();
        SetSoundSetting(userSettingsData.IsSoundEnable);
    }

    private void SetSoundSetting(bool isEnable)
    {
        _soundOnToggle.SetActive(isEnable);
        _soundOffToggle.SetActive(!isEnable);
    }
}
