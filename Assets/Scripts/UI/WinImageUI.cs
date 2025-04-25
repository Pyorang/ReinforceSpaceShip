using UnityEngine;

public class WinImageUI : BaseUI
{
    public void OnEnable()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.Play(AudioType.BGM, "Win");
    }

    public override void OnClickCloseButton()
    {
        UserDataManager.Instance.GetUserData<UserChapterData>().CurrentChapterNum++;
        UserDataManager.Instance.GetUserData<UserChapterData>().SaveData();

        SceneLoader.Instance.LoadSceneAsync(ESceneType.Lobby);

        base.OnClickCloseButton();
    }
}
