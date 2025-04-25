using UnityEngine;

public class DefeatImageUI : BaseUI
{
    public void OnEnable()
    {
        Time.timeScale = 0f;
        AudioManager.Instance.Play(AudioType.BGM, "Defeat");
    }

    public override void OnClickCloseButton()
    {
        SceneLoader.Instance.LoadSceneAsync(ESceneType.Lobby);

        base.OnClickCloseButton();
    }
}
