
namespace Dal.DataImplementation.CRUDInterfaces;
public interface ICommonGroupDataCRUD
{
    Task<string> CreateAsync(CommonGroupData commonGroupData);
    Task<bool> DeleteAsync(string managerId);
    Task<CommonGroupData> ReadAsync(string managerId);
    Task<bool> UpdateAsync(CommonGroupData commonGroupData);
}
