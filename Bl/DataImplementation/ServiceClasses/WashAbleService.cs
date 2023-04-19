
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
    public Task<string> CreateObject(WashAbleDTO washAbleDTO)
    {
        try
        {
            WashAble washAble = MapWashAbleDTO_washAble(washAbleDTO);
            washAble.ID = "";
            return _washAbleCRUD.CreateAsync(washAble);
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (ExistsDataObjectExceotion ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
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
        catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NullReferenceException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    #region Delete function
    public Task<bool> DeleteObject(string id)
    {
        try
        {
            return _washAbleCRUD.DeleteAsync(id);
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(WashAbleDTO washAbleDTO)
    {
        try
        {
            WashAble washAble = MapWashAbleDTO_washAble(washAbleDTO);
            return await _washAbleCRUD.UpdateAsync(washAble);
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    #region GetAll function
    public async Task<List<WashAbleDTO>> GetAll(string userId)
    {
        try
        {
            List<WashAbleDTO> washAblesBl = new();
            List<WashAble> washAbles = await _washAbleCRUD.ReadAllAsync(userId);
            //washAbles.ForEach(w => washAblesBl.Add(MapWashAble_washAbleDTO(w)));
            washAblesBl = _mapper.Map<List<WashAble> , List<WashAbleDTO>>(washAbles);
          //  washAblesBl = MapWashAble_washAbleDTO()
            return washAblesBl;
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion



    public WashAbleDTO MapWashAble_washAbleDTO(WashAble washAble) => _mapper.Map<WashAbleDTO>(washAble);
  /*  {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<WashAble, WashAbleDTO>());
        var mapper = config.CreateMapper();
        return mapper.Map<WashAbleDTO>(washAble);
    }*/

    public WashAble MapWashAbleDTO_washAble(WashAbleDTO washAbleDTO) => _mapper.Map<WashAble>(washAbleDTO);
/*        {
        var config = new MapperConfiguration(cfg => cfg.CreateMap<WashAbleDTO, WashAble>());
        var mapper = config.CreateMapper();
        return mapper.Map<WashAble>(washAbleDTO);
    }*/
  

    public List<WashAbleDTO> GetWashAblesItems(List<string> washAbleIDs)
    {
        try
        {
            List<WashAbleDTO> washAbles = new();
            washAbleIDs.ForEach(async washAble =>
            {
                washAbles.Add(await GetObject(washAble));
            });
            return washAbles;//exit
        }
        catch (AggregateException ex) { throw new Exception(ex.Message); }

    }

    public List<string> GetWashAblesId(List<WashAbleDTO> washAbles)
    {
        List<string> washAbleIds = new();
        washAbles.ForEach(washable =>
        {
            washAbleIds.Add(washable.ID);
        });
        return washAbleIds;
    }

}



















