
namespace BL.DataImplementation.ServiceClasses;
public class LaundryService : ILaundryService
{
    readonly IMapper _mapper;
    readonly ILaundryCRUD _laundryCRUD;
    public LaundryService(IMapper mapper, ILaundryCRUD laundryCRUD)
    {
        _mapper = mapper;
        _laundryCRUD = laundryCRUD;
    }

    #region Create function
    public Task<string> CreateObject(LaundryDTO laundryDTO)
    {
        try
        {
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
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region GetAll function
    public async Task<List<LaundryDTO>> GetAll(string managerId)
    {
        try
        {
            List<Laundry> laundries = await _laundryCRUD.ReadAllAsync(managerId);
            return _mapper.Map<List<LaundryDTO>>(laundries);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Mapping function
    public LaundryDTO MapLaundry_LaundryDTO(Laundry laundry) => _mapper.Map<LaundryDTO>(laundry);


    public Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO) => _mapper.Map<Laundry>(laundryDTO);
    #endregion

}
