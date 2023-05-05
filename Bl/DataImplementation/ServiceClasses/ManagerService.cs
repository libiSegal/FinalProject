
using Bl.DataAPI;

namespace BL.DataImplementation.ServiceClasses;

public class ManagerService : IManagerService
{
    private readonly IUserService _userService;
    private readonly ILaundryService _laundryService;
    private readonly IWashAbleService _washAbleServise;
    private readonly IManagerCRUD _managerCRUD;
    private readonly IMapper _mapper;
    public ManagerService(IUserService userService, IWashAbleService washAbleServise, ILaundryService laundryService, IManagerCRUD managerServise, IMapper mapper)
    {
        _userService = userService;
        _washAbleServise = washAbleServise;
        _laundryService = laundryService;
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
            manager.WashingMachine = new(managerDTO.WashingMachineDTO.Company, managerDTO.WashingMachineDTO.Model, managerDTO.WashingMachineDTO.LaundryWeight);
            manager.ID = "";
            return await _managerCRUD.CreateAsync(manager);
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (ExistsDataObjectExceotion ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
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
        catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NullReferenceException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }

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
        catch(Exception ex) { throw new GlobalException(ex);}
        /*catch (TimeoutException ex) { throw ex; }
        catch (MongoConnectionException ex) { throw ex; }
        catch (NullReferenceException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
*/
    }
    #endregion

    #region Delete function
    public async Task<bool> DeleteObject(string id)
    {
        try
        {

            _userService.GetAll(id).Result.ForEach(user => _userService.DeleteObject(user.ID));
            _washAbleServise.GetAll(id).Result.ForEach(washAble => _washAbleServise.DeleteObject(washAble.ID));
            _laundryService.GetAll(id).Result.ForEach(laundry => _laundryService.DeleteObject(laundry.ID));
            return await _managerCRUD.DeleteAsync(id);
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(ManagerDTO managerDTO)
    {
        try
        {
           
            Manager managerFromDB = await _managerCRUD.ReadAsync(managerDTO.ID);
            Manager managerToUpdate = MapManagerDTO_Manager(managerDTO);
            managerToUpdate.ID = managerFromDB.ID;
            return await _managerCRUD.UpdateAsync(managerToUpdate);
        }
        catch (TimeoutException ex) { throw ex; }
        catch (MongoWriteException ex) { throw ex; }
        catch (MongoBulkWriteException ex) { throw ex; }
        catch (NotExistsDataObjectException ex) { throw ex; }
        catch (Exception ex) { throw new Exception(ex.Message); }
    }
    #endregion

    #region Mapping functions
    public async Task<ManagerDTO> MapManager_ManagerDTO(Manager manager)
    {
        ManagerDTO managerDTO = _mapper.Map<ManagerDTO>(manager);
        managerDTO.UsersDTO = await _userService.GetAll(manager.ID);
        managerDTO.Items = await _washAbleServise.GetAll(manager.ID);
        managerDTO.LaundriesDTO = await _laundryService.GetAll(manager.ID);
        return managerDTO;
    }

    public Manager MapManagerDTO_Manager(ManagerDTO managerDTO) => _mapper.Map<Manager>(managerDTO);

    #endregion
}
