public interface IUserData
{
    /// <summary>
    /// �⺻������ �����Ѵ�.
    /// </summary>
    void SetDefaultData();

    /// <summary>
    /// �����͸� �ҷ��´�.
    /// </summary>
    /// <returns>�����ߴٸ� true, �����ߴٸ� false</returns>
    bool LoadData();

    /// <summary>
    /// �����͸� �����Ѵ�.
    /// </summary>
    /// <returns>�����ߴٸ� true, �����ߴٸ� false</returns>
    bool SaveData();
}