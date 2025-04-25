using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using DG.Tweening;

public class TitleManager : MonoBehaviour
{
    [SerializeField] private GameObject logoImage;
    [SerializeField] private GameObject loadingText;

    private void Awake()
    {
        logoImage.SetActive(true);
        loadingText.SetActive(false);
    }

    private void Start()
    {
        if (UserDataManager.Instance.ExistsSavedData)
        {
            UserDataManager.Instance.LoadUserData();
        }
        else
        {
            UserDataManager.Instance.SetDefaultData();
            UserDataManager.Instance.SaveUserData();
        }

        AudioManager.Instance.SyncUserSettings();
    }


    public void LogoAnimationEnded()
    {
        StartCoroutine(LoadingSequence());
    }

    private IEnumerator LoadingSequence()
    {
        logoImage.gameObject.SetActive(false);
        loadingText.SetActive(true);

        var loadingOperation = SceneLoader.Instance.LoadSceneAsync(ESceneType.Lobby);
        if (loadingOperation == null)
        {
            yield break;
        }

        loadingOperation.allowSceneActivation = false;
        yield return new WaitForSeconds(0.5f);

        while (true)
        {
            if (loadingOperation.isDone)
            {
                break;
            }

            if (loadingOperation.progress >= 0.9f)
            {
                loadingOperation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}