public interface IUserData
{
    /// <summary>
    /// 기본값으로 설정한다.
    /// </summary>
    void SetDefaultData();

    /// <summary>
    /// 데이터를 불러온다.
    /// </summary>
    /// <returns>성공했다면 true, 실패했다면 false</returns>
    bool LoadData();

    /// <summary>
    /// 데이터를 저장한다.
    /// </summary>
    /// <returns>성공했다면 true, 실패했다면 false</returns>
    bool SaveData();
}