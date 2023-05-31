
namespace BL.DataImplementation.ServiceClasses;
public class ManagerService : IManagerService
{
    private readonly IUserService _userService;
    private readonly ICommonGroupDataService _commonGroupDataService;
    private readonly IWashAbleService _washAbleServise;
    private readonly IManagerCRUD _managerCRUD;
    private readonly IMapper _mapper;
    public ManagerService(IUserService userService, IWashAbleService washAbleServise,/* ILaundryService laundryService,*/ ICommonGroupDataService commonGroupDataService, IManagerCRUD managerServise, IMapper mapper)
    {
        _userService = userService;
        _washAbleServise = washAbleServise;
        //  _laundryService = laundryService;
        _commonGroupDataService = commonGroupDataService;
        _managerCRUD = managerServise;
        _mapper = mapper;
    }
    #region Create function
    public async Task<string> CreateObject(ManagerDTO managerDTO)
    {
        //It is necessary to make sure that mapping work
        try
        {           
            Manager manager = MapManagerDTO_Manager(managerDTO);
            manager.ID = string.Empty;
            string managerId = await _managerCRUD.CreateAsync(manager);
            CommonGroupDataDTO commonGroupDataDTO = new() { ManagerID = managerId};
            await _commonGroupDataService.CreateObject(commonGroupDataDTO);
            return managerId;
        }
        catch (ExistsDataObjectExceotion ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Get by id function
    public async Task<ManagerDTO> GetObject(string id)
    {
        try
        {
            Manager manager = await _managerCRUD.ReadAsync(id);
            return await MapManager_ManagerDTO(manager);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }

    }
    #endregion

    #region Get by name and password
    public async Task<ManagerDTO> GetObject(string name, string password)
    {
        try
        {
            Manager manager = await _managerCRUD.ReadAsync(name, password);
            return await MapManager_ManagerDTO(manager);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }

    }
    #endregion

    #region Delete function
    public async Task<bool> DeleteObject(string id)
    {//must!!!!! to check if users and washAbles delete are delete without wait for them.
        try
        {
            _washAbleServise.GetAll(id).Result.ForEach(washAble => _washAbleServise.DeleteObject(washAble.ID));
            _userService.GetAll(id).Result.ForEach(user => _userService.DeleteObject(user.ID));
            await _commonGroupDataService.DeleteObject(id);
          //  _laundryService.GetAll(id).Result.ForEach(laundry => _laundryService.DeleteObject(laundry.ID));
            return await _managerCRUD.DeleteAsync(id);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(ManagerDTO managerDTO)
    {
        try
        {
            Manager managerToUpdate = MapManagerDTO_Manager(managerDTO);

            if (!_commonGroupDataService.UpdateObject(managerDTO.CommonData).Result)
                throw new Exception("Common data return with error");
               
            return await _managerCRUD.UpdateAsync(managerToUpdate);

        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Mapping functions
    public async Task<ManagerDTO> MapManager_ManagerDTO(Manager manager)
    {//need to do a list<task> for all this awaits.
        
        ManagerDTO managerDTO = _mapper.Map<ManagerDTO>(manager);
        managerDTO.UsersDTO = await _userService.GetAll(manager.ID);
        managerDTO.Items = await _washAbleServise.GetAll(manager.ID);
        managerDTO.CommonData = await _commonGroupDataService.GetObject(manager.ID);
      //managerDTO.LaundriesDTO = await _laundryService.GetAll(manager.ID);
        return managerDTO;
    }

    public Manager MapManagerDTO_Manager(ManagerDTO managerDTO) => _mapper.Map<Manager>(managerDTO);

    #endregion
}
