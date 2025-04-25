using UnityEngine;

public class InGameManager : SingletonBehaviour<InGameManager>
{
    public InGameUIController _inGameUIController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        _inGameUIController = FindAnyObjectByType<InGameUIController>();
        if (_inGameUIController == null)
        {
            return;
        }

        _inGameUIController.Init();
        AudioManager.Instance.Play(AudioType.BGM, "inGame");
    }

    public void StartBossMode()
    {
        _inGameUIController.SetBossAlertText();
    }
}
