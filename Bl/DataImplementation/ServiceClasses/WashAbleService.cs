
namespace BL.DataImplementation.ServiceClasses;
public class WashAbleService : IWashAbleService
{
    readonly IWashAbleCRUD _washAbleCRUD;
    readonly IMapper _mapper;
    public WashAbleService( IMapper mapper , IWashAbleCRUD washAbleCRUD )
    {
        _mapper = mapper;
        _washAbleCRUD = washAbleCRUD;
    }
    #region Create function
    public  Task<string> CreateObject(WashAbleDTO washAbleDTO)
    {
        try
        {
            
            if(washAbleDTO.Weight == 0)
            {
                List<WashAbleDTO> allWashAbles = GetAll(washAbleDTO.UserId).Result;
                double averageWeight = 0;
                allWashAbles.ForEach(w => { averageWeight += w.Weight;});
                washAbleDTO.Weight = averageWeight / allWashAbles.Count;
            }
            WashAble washAble = MapWashAbleDTO_washAble(washAbleDTO);
            washAble.ID = string.Empty;
            return _washAbleCRUD.CreateAsync(washAble);
        }
        catch (ExistsDataObjectExceotion ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Get function
    public async Task<WashAbleDTO> GetObject(string id)
    {//id = barcode.data.id
        try
        {
            WashAble washAble = await _washAbleCRUD.ReadAsync(id);
            return MapWashAble_washAbleDTO(washAble);
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
            return _washAbleCRUD.DeleteAsync(id);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(WashAbleDTO washAbleDTO)
    {
        try
        {
           WashAbleDTO lastWashAble = GetObject(washAbleDTO.ID).Result;
            if (washAbleDTO.Status == Status.clean && lastWashAble.Status == Status.dirty)
            {
                washAbleDTO.PrevWash.Add(DateTime.Now);
                washAbleDTO.EnteryDate = default(DateTime);
                washAbleDTO.NecessityLevel = NecessityLevel.standard;               
            }
            else  if(washAbleDTO.Status == Status.dirty && lastWashAble.Status == Status.clean)
                washAbleDTO.EnteryDate = DateTime.Now;
                
            WashAble washAble = MapWashAbleDTO_washAble(washAbleDTO);
            return await _washAbleCRUD.UpdateAsync(washAble);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region GetAll function
    public async Task<List<WashAbleDTO>> GetAll(string userId)
    {
        try
        {
            List<WashAbleDTO> washAblesBl = new();
            List<WashAble> washAbles = await _washAbleCRUD.ReadAllAsync(userId);
            washAblesBl = _mapper.Map<List<WashAble> , List<WashAbleDTO>>(washAbles);
            return washAblesBl;
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Mapping functions

    public WashAbleDTO MapWashAble_washAbleDTO(WashAble washAble) => _mapper.Map<WashAbleDTO>(washAble);
    public WashAble MapWashAbleDTO_washAble(WashAbleDTO washAbleDTO) => _mapper.Map<WashAble>(washAbleDTO);
    #endregion

    #region Get wash able items
    public List<WashAbleDTO> GetWashAblesItems(List<string> washAbleIDs)
    {
        try
        {
            List<Task<WashAbleDTO>> washAblesTask = new();
            washAbleIDs.ForEach(washAble =>
            {
                washAblesTask.Add(GetObject(washAble));
            });
            List<WashAbleDTO> washAbles = ( Task.WhenAll(washAblesTask)).Result.ToList();
            return washAbles;
        }
        catch (AggregateException ex) { throw new BLException(ex); }
    }
    #endregion
}



















