namespace Dal.DataImplementation.CRUDClasses
{
    public interface ICommonGroupDataCRUD
    {
        Task<string> CreateAsync(CommonGroupData commonGroupData);
        Task<bool> DeleteAsync(string id);
        Task<CommonGroupData> ReadAsync(string managerId);
        Task<bool> UpdateAsync(CommonGroupData commonGroupData);
    }
}