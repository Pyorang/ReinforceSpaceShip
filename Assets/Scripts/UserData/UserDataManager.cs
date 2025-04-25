using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserDataManager : SingletonBehaviour<UserDataManager>
{
    public bool ExistsSavedData { get; private set; } = false;
    public List<IUserData> UserDataList { get; private set; } = new();

    protected override void Init()
    {
        base.Init();

        ExistsSavedData = PlayerPrefs.GetInt(nameof(ExistsSavedData)) == 1;

        UserDataList.Add(new UserSettingsData());
        UserDataList.Add(new UserChapterData());
    }

    public void SetDefaultData()
    {
        foreach (var data in UserDataList)
        {
            data.SetDefaultData();
        }
    }

    public void LoadUserData()
    {
        if (ExistsSavedData)
        {
            foreach (var data in UserDataList)
            {
                data.LoadData();
            }
        }
    }

    public void SaveUserData()
    {
        bool hasError = false;
        foreach (var data in UserDataList)
        {
            if (false == data.SaveData())
            {
                hasError = true;
                break;
            }
        }

        if (hasError == false)
        {
            PlayerPrefs.SetInt(nameof(ExistsSavedData), 1);
            PlayerPrefs.Save();

            ExistsSavedData = true;
        }
    }

    public T GetUserData<T>() where T : class, IUserData
    {
        return UserDataList.OfType<T>().FirstOrDefault();
    }
}
