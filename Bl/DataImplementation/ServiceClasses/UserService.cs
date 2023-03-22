using AutoMapper;
using Dal;
using Dal.Exceptions;
using MongoDB.Driver;


namespace BL
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserCRUD _userService;
        private readonly IWashAbleService _washAbleService;
        public UserService(IUserCRUD userService, IWashAbleService washAbleService,  IMapper mapper)
        {
            _userService = userService;
            _washAbleService = washAbleService;
            _mapper = mapper;
        }
        public Task<string> CreateObject(UserDTO userBl)
        {
            try
            {
                User user = MapUserDTO_User(userBl);
                user.ID = "";
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
                return MapUser_UserDTO(user);
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
                return MapUser_UserDTO(user);
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
                 _washAbleService.GetAll(id).Result.ForEach(item => _washAbleService.DeleteObject(item.ID));
                return await _userService.DeleteAsync(id);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }
        public async Task<bool> UpdateObject(UserDTO userDTO)
        {
            try
            {
               /* User userFromDB = await _userService.ReadAsync(id);*/
                User user = MapUserDTO_User(userDTO);
                /*user.ID = userFromDB.ID;*/
                return await _userService.UpdateAsync(user);
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }

        public async Task<List<UserDTO>> GetAll(string managerId)
        {
            try
            {
                List<UserDTO> usersBl = new();
                List<User> users = await _userService.ReadAllAsync(managerId);
                users.ForEach(u => usersBl.Add( MapUser_UserDTO(u)));
                return usersBl;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



        public  UserDTO MapUser_UserDTO(User user) => _mapper.Map<UserDTO>(user);
       
        public User MapUserDTO_User(UserDTO userDTO)// => _mapper.Map<User>(userDTO);
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, User>());
            var mapper = config.CreateMapper();
            return mapper.Map<User>(userDTO);
        }
     
   /*     public async Task<List<string>> UpdateUsersList(ManagerDTO managerDTO, Manager managerFromDB)
        {
            try
            {
                List<string> resultList = new();
                List<Task<bool>> deleteTasks = new();
                List<Task<string>> createTasks = new();
               
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
                    Task.WhenAll(deleteTasks);
                }
                //check if we need to update one or more items;
                List<UserDTO> usersForManagerFromDB = await GetAllUsers(managerFromDB.ID);
                managerDTO.UsersDTO.Except(usersForManagerFromDB).ToList().
                    ForEach(async user => await UpdateObject(user, user.ID));
                return resultList;
            }


            catch (TimeoutException ex) { throw ex; }
            catch (MongoWriteException ex) { throw ex; }
            catch (MongoBulkWriteException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }

        }*/

    }
}