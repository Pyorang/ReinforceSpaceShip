using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class UserDataManger : SingletonBehaviour<UserDataManger>
{
    public bool ExistsSavedData { get; private set; } = false;
    public List<IUserData> UserDataList { get; private set; } = new();

    protected override void Init()
    {
        base.Init();

        ExistsSavedData = PlayerPrefs.GetInt(nameof(ExistsSavedData)) == 1;

        UserDataList.Add(new UserSettingsData());
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
#if DEV_VER
        // NOTE : 추후 플레이중 PlayerPrefs 데이터를 지우면서 다시 로드하는 일도 생길 수 있어 매번 체크한다.
        ExistsSavedData = (PlayerPrefs.GetInt(nameof(ExistsSavedData)) == 1);
#endif

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
