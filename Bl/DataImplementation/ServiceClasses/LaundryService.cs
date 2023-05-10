
namespace BL.DataImplementation.ServiceClasses;
public class LaundryService : ILaundryService
{
    readonly IMapper _mapper;
    readonly ILaundryCRUD _laundryCRUD;
    readonly IWashAbleService _washAbleService;
    public LaundryService(IMapper mapper, ILaundryCRUD laundryCRUD, IWashAbleService washAbleService)
    {
        _mapper = mapper;
        _laundryCRUD = laundryCRUD;
        _washAbleService = washAbleService;
    }

    #region Create function
    public Task<string> CreateObject(LaundryDTO laundryDTO)
    {
        try
        {
            laundryDTO.ID = string.Empty;
            laundryDTO.Date = DateTime.Now;
            laundryDTO.WashAbles.ForEach(w => { w.Status = Status.clean; _washAbleService.UpdateObject(w); });
            Laundry laundry = MapLaundryDTO_Laundry(laundryDTO);
            return _laundryCRUD.CreateAsync(laundry);
        }
        catch (ExistsDataObjectExceotion ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Get function
    public async Task<LaundryDTO> GetObject(string id)
    {
        try
        {
            Laundry laundry = await _laundryCRUD.ReadAsync(id);
            return MapLaundry_LaundryDTO(laundry);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Delete function
    public Task<bool> DeleteObject(string id)
    {
        try
        {
            return _laundryCRUD.DeleteAsync(id);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(LaundryDTO laundryDTO)
    {
        try
        {
            return await _laundryCRUD.UpdateAsync(MapLaundryDTO_Laundry(laundryDTO));
        }
        catch (Exception ex) { throw new BLException(ex, 500); }
    }
    #endregion

    #region GetAll function
    public async Task<List<LaundryDTO>> GetAll(string managerId)
    {
        try
        {
            List<Laundry> laundries = await _laundryCRUD.ReadAllAsync(managerId);
            return laundries.Select(l => MapLaundry_LaundryDTO(l)).ToList();
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Mapping function
    public LaundryDTO MapLaundry_LaundryDTO(Laundry laundry) 
    {
        LaundryDTO laundryDTO = _mapper.Map<LaundryDTO>(laundry);
        laundryDTO.WashAbles = _washAbleService.GetWashAblesItems(laundry.WashAblesIDs);
        return laundryDTO;
    }
    public Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO) => _mapper.Map<Laundry>(laundryDTO);
    #endregion
}