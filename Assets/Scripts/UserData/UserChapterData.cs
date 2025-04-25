using System;
using UnityEngine;

public class UserChapterData : IUserData
{
    public int CurrentChapterNum { get; set; }
    public void SetDefaultData()
    {
        CurrentChapterNum = 1;
    }

    public bool LoadData()
    {
        bool result = false;
        try
        {
            CurrentChapterNum = PlayerPrefs.GetInt(nameof(CurrentChapterNum));

            result = true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return result;
    }

    public bool SaveData()
    {
        bool result = false;
        try
        {
            PlayerPrefs.SetInt(nameof(CurrentChapterNum), CurrentChapterNum);
            PlayerPrefs.Save();

            result = true;
        }
        catch (Exception e)
        {
            Debug.LogException(e);
        }

        return result;
    }
}
