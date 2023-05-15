
namespace BL.DataImplementation.ServiceClasses;
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
    #region Create function
    public Task<string> CreateObject(UserDTO userDTO)
    {
        try
        {
            User user = MapUserDTO_User(userDTO);
            user.ID = String.Empty;
            return _userService.CreateAsync(user);
        }
        catch (ExistsDataObjectExceotion ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }

    }
    #endregion

    #region Get by name and password function
    public async Task<UserDTO> GetObject(string name, string password)
    {
        try
        {
            User user = await _userService.ReadAsync(name, password);
            return MapUser_UserDTO(user);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Get by id function
    public async Task<UserDTO> GetObject(string id)
    {
        try
        {
            User user = await _userService.ReadAsync(id);
            return MapUser_UserDTO(user);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }

    }
    #endregion

    #region Delete function
    public async Task<bool> DeleteObject(string id)
    {
        try
        {
            _washAbleService.GetAll(id).Result.ForEach(item => _washAbleService.DeleteObject(item.ID));
            return await _userService.DeleteAsync(id);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Update function
    public async Task<bool> UpdateObject(UserDTO userDTO)
    {
        try
        {
            User user = MapUserDTO_User(userDTO);
            return await _userService.UpdateAsync(user);
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region GetAll function
    public async Task<List<UserDTO>> GetAll(string managerId)
    {
        try
        {
            List<User> users = await _userService.ReadAllAsync(managerId);
            return users.Select(u => MapUser_UserDTO(u)).ToList();
        }
        catch (NotExistsDataObjectException ex) { throw new BLException(ex, 400); }
        catch (Exception ex) { throw new BLException(ex); }
    }
    #endregion

    #region Mapping function
    public UserDTO MapUser_UserDTO(User user)
    {
        UserDTO userDTO = _mapper.Map<UserDTO>(user);
        userDTO.Items = _washAbleService.GetAll(userDTO.ID).Result;
        return userDTO;
    }

    public User MapUserDTO_User(UserDTO userDTO) => _mapper.Map<User>(userDTO);
    #endregion
}