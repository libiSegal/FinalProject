
namespace BL.DataImplementation.ServiceClasses;

public class CommonGroupDataService : ICommonGroupDataService
{
    readonly IMapper _mapper;
    readonly ICommonGroupDataCRUD _commonGroupDataCRUD;
    public CommonGroupDataService(IMapper mapper, ICommonGroupDataCRUD commonGroupDataCRUD)
    {
        _mapper = mapper;
        _commonGroupDataCRUD = commonGroupDataCRUD;
    }

    #region Create function
    public Task<string> CreateObject(CommonGroupDataDTO commonGroupDataDTO)
    {
        try
        {
            commonGroupDataDTO.ID = string.Empty;

            CommonGroupData commonGroupData = MapCommonGroupDataDTO_CommonGroupData(commonGroupDataDTO);
            return _commonGroupDataCRUD.CreateAsync(commonGroupData);
        }
        catch (ExistsDataObjectExceotion ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Get function
    public async Task<CommonGroupDataDTO> GetObject(string id)
    {
        try
        {
            CommonGroupData commonGroupData = await _commonGroupDataCRUD.ReadAsync(id);
            return MapCommonGroupData_CommonGroupDataDTO(commonGroupData);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Delete function
    public Task<bool> DeleteObject(string managerId)
    {
        try
        {
            return _commonGroupDataCRUD.DeleteAsync(managerId);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(CommonGroupDataDTO commonGroupDataDTO)
    {
        try
        {
            return await _commonGroupDataCRUD.UpdateAsync(MapCommonGroupDataDTO_CommonGroupData(commonGroupDataDTO));
        }
        catch (Exception ex) { throw new BLException(ex, 500); }
    }
    #endregion

    #region Mapping function
    public CommonGroupDataDTO MapCommonGroupData_CommonGroupDataDTO(CommonGroupData commonGroupData) => _mapper.Map<CommonGroupDataDTO>(commonGroupData);

    public CommonGroupData MapCommonGroupDataDTO_CommonGroupData(CommonGroupDataDTO commonGroupDataDTO) => _mapper.Map<CommonGroupData>(commonGroupDataDTO);
    #endregion
}