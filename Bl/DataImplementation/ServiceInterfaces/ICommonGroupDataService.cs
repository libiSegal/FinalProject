
namespace BL.DataImplementation.ServiceInterfaces;
public interface ICommonGroupDataService
{
    Task<string> CreateObject(CommonGroupDataDTO commonGroupDataDTO);
    Task<bool> DeleteObject(string id);
    Task<CommonGroupDataDTO> GetObject(string id);
    CommonGroupDataDTO MapCommonGroupData_CommonGroupDataDTO(CommonGroupData commonGroupData);
    Task<bool> UpdateObject(CommonGroupDataDTO commonGroupDataDTO);
}
