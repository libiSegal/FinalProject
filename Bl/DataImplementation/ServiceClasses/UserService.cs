using AutoMapper;
using Dal;
using Dal.Exceptions;
using MongoDB.Driver;


namespace BL
{
    public class UserService : IUserService
    {
        private readonly IUserCRUD _userService;
        private readonly IWashAbleService _washAbleAction;
        public UserService(IUserCRUD userService, IWashAbleService washAbleAction)
        {
            _userService = userService;
            _washAbleAction = washAbleAction;
        }
        public Task<string> CreateObject(UserDTO userBl)
        {
            try
            {
                User user = MapUserDTO_User(userBl);
                user.ID = "";
                user.ItemsId = new();
                return _userService.CreateAsync(user);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (ExistsDataObjectExceotion ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        public async Task<UserDTO> GetObject(string name, string password)
        {
            try
            {
                User user = await _userService.ReadAsync(password, name);
                return await MapUser_UserDTO(user);
                /*return new UserDTO();*/
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        public async Task<UserDTO> GetObject(string id)
        {
            try
            {
                User user = await _userService.ReadAsync(id);
                return await MapUser_UserDTO(user);
                /*return new UserDTO();*/
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NullReferenceException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }
        public async Task<bool> DeleteObject(string id)
        {
            try
            {
                User user = await _userService.ReadAsync(id);
                user.ItemsId.ForEach(item => _washAbleAction.DeleteObject(item));
                return await _userService.DeleteAsync(user.ID);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<bool> UpdateObject(UserDTO userDTO, string id)
        {
            try
            {
                User userFromDB = await _userService.ReadAsync(id);
                User user = MapUserDTO_User(userDTO);
                user.ID = userFromDB.ID;
                user.ItemsId = await _washAbleAction.UpdateItemsList(userDTO, userFromDB);
                //  _washAbleAction.GetWashAblesId(await _washAbleAction.GetAllWashAbles(user.Id)) ;
                return await _userService.UpdateAsync(user);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<List<UserDTO>> GetAllUsers(string managerId)
        {
            try
            {
                List<UserDTO> usersBl = new();
                List<User> users = await _userService.ReadAllAsync(managerId);
                users.ForEach(async u => usersBl.Add(await MapUser_UserDTO(u)));
                return usersBl;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



        public async Task<UserDTO> MapUser_UserDTO(User user)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<User, UserDTO>());
            var mapper = config.CreateMapper();
            var a = mapper.Map<UserDTO>(user);
            a.Items = await _washAbleAction.GetAll(user.ID);
            return a;
        }
        public User MapUserDTO_User(UserDTO userDTO)
        {
            var mapper = new MapperConfiguration(cfg => cfg.AddProfile<UserDTOManagementProfile>())
            .CreateMapper();
            return mapper.Map<User>(userDTO);
        }

        public async Task<List<string>> UpdateUsersList(ManagerDTO managerDTO, Manager managerFromDB)
        {
            try
            {
                List<string> resultList = new();
                List<Task<bool>> deleteTasks = new();
                List<Task<string>> createTasks = new();

               /* //check if manager delete all users;
                if (managerDTO.UsersDTO == null)
                {
                    managerFromDB.UsersID.ForEach(user => deleteTasks.Add(_userService.DeleteAsync(user)));
                    Task.WhenAll(deleteTasks);
                    return resultList;
                }

                //check if the 
                if (managerFromDB.UsersID == null)
                {
                    managerDTO.UsersDTO.ForEach(user => createTasks.Add(_userService.CreateAsync(MapUserDTO_User(user))));
                    resultList.AddRange(Task.WhenAll(createTasks).Result);
                    return resultList;
                }*/
                //check if we need to add new users;
                List<UserDTO> usersToCreate = managerDTO.UsersDTO.FindAll(user => user.ID == "");
                if (usersToCreate != null)
                {
                    usersToCreate.ForEach(user => createTasks.Add(_userService.CreateAsync(MapUserDTO_User(user))));
                }

                managerDTO.UsersDTO.RemoveAll(item => item.ID == "");
                resultList.AddRange(Task.WhenAll(createTasks).Result);
                managerDTO.UsersDTO.ForEach(user => resultList.Add(user.ID));

                //check if we need to delete a item;
                List<string> itemsIdToRemove = managerFromDB.UsersID.Except(managerDTO.UsersDTO.Select(user => user.ID).ToList()).ToList();

                if (itemsIdToRemove != null)
                {
                    itemsIdToRemove.ForEach(user => deleteTasks.Add(_userService.DeleteAsync(user)));
                    //Task.WhenAll(deleteTasks);
                }
                //check if we need to update one or more items;
                List<UserDTO> usersForManagerFromDB = await GetAllUsers(managerFromDB.ID);
                managerDTO.UsersDTO.Except(usersForManagerFromDB).ToList().
                    ForEach(user => _userService.UpdateAsync(MapUserDTO_User(user)));
                return resultList;
            }


            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }

    }
}