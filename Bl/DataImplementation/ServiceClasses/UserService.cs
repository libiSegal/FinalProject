﻿using AutoMapper;
using Bl;
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
        public UserService(IUserCRUD userService, IWashAbleService washAbleService, IMapper mapper)
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
                users.ForEach(u => usersBl.Add(MapUser_UserDTO(u)));
                return usersBl;
            }
            catch (TimeoutException ex) { throw ex; }
            catch (MongoConnectionException ex) { throw ex; }
            catch (NotExistsDataObjectException ex) { throw ex; }
            catch (Exception ex) { throw new Exception(ex.Message); }
        }



        public UserDTO MapUser_UserDTO(User user) => _mapper.Map<UserDTO>(user);

        public User MapUserDTO_User(UserDTO userDTO) => _mapper.Map<User>(userDTO);




    }
}