using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LobbyManager : SingletonBehaviour<LobbyManager>
{
    public LobbyUiController LobbyUiController { get; private set; }

    protected override void Init()
    {
        IsDestroyOnLoad = true;

        base.Init();
    }

    private void Start()
    {
        LobbyUiController = FindAnyObjectByType<LobbyUiController>();
        if (LobbyUiController == null)
        {
            return;
        }

        LobbyUiController.Init();
        AudioManager.Instance.Play(AudioType.BGM, "lobby");
    }
}
