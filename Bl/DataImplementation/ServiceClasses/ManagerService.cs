using AutoMapper;
using Bl;
using Dal;
using Dal.Exceptions;
using MongoDB.Driver;


namespace BL
{
    public class ManagerService : IManagerService
    {
        private readonly IUserService _userService;
        private readonly ILaundryService _laundryService;
        private readonly IWashAbleService _washAbleServise;
        private readonly IManagerCRUD _managerCRUD;
        private readonly IMapper _mapper;
        public ManagerService(IUserService userService, IWashAbleService washAbleServise, ILaundryService laundryService, IManagerCRUD managerServise , IMapper mapper)
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
            try
            {
                Manager manager = MapManagerDTO_Manager(managerDTO);
                manager.ID = "";
                manager.ItemsId = new();
                manager.LaundriesID = new();
                manager.UsersID = new();
                return await _managerCRUD.CreateAsync(manager);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Read by id function
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

        #region Read by name and password
        public async Task<ManagerDTO> GetObject(string name, string password)
        {
            try
            {
                Manager manager = await _managerCRUD.ReadAsync(name, password);
                return await MapManager_ManagerDTO(manager);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        #endregion

        #region Delete function
        public async Task<bool> DeleteObject(string id)
        {
            try
            {
                Manager manager = await _managerCRUD.ReadAsync(id);
                manager.ItemsId.ForEach(item => _washAbleServise.DeleteObject(item));
                manager.UsersID.ForEach(user => _userService.DeleteObject(user));
                manager.LaundriesID.ForEach(laundry => _laundryService.DeleteObject(laundry));
                return await _managerCRUD.DeleteAsync(manager.ID);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        #endregion

        #region Update function
        public async Task<bool> UpdateObject(ManagerDTO managerDTO, string id)
        {
            try
            {
                Manager managerFromDB = await _managerCRUD.ReadAsync(id);
                Manager managerToUpdate = MapManagerDTO_Manager(managerDTO);
                managerToUpdate.ID = managerFromDB.ID;
                managerToUpdate.UsersID = await _userService.UpdateUsersList(managerDTO, managerFromDB);
                managerToUpdate.LaundriesID = await _laundryService.UpdateLaudryList(managerDTO, managerFromDB);
                managerToUpdate.ItemsId = await _washAbleServise.UpdateItemsList(managerDTO, managerFromDB);
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
            try
            {
                WashingMachineDTO washingMachineDTO = new MapperConfiguration(cfg => cfg.CreateMap<WashingMachine, WashingMachineDTO>()).CreateMapper().Map<WashingMachineDTO>(manager.WashingMachine);
                CalendarDTO calendarDTO = new MapperConfiguration(cfg => cfg.CreateMap<Calendar, CalendarDTO>()).CreateMapper().Map<CalendarDTO>(manager.Calendar);
                ManagerDTO managerDTO = new ManagerDTO(manager.Name, manager.Password, washingMachineDTO, calendarDTO);
                managerDTO.ID = manager.ID;
                managerDTO.UsersDTO = await _userService.GetAllUsers(manager.ID);
                managerDTO.Items = await _washAbleServise.GetAll(manager.ID);
                managerDTO.LaundriesDTO = await _laundryService.GetAll(manager.ID);
                return managerDTO;
            }
            catch (Exception ex) { throw new Exception(ex.Message); }


        }
        public Manager MapManagerDTO_Manager(ManagerDTO managerDTO)
        {
            return new MapperConfiguration(cfg => cfg.AddProfile<ManagerDTOManagementProfile>())
                  .CreateMapper().Map<Manager>(managerDTO);


        }
        #endregion
    }
}
