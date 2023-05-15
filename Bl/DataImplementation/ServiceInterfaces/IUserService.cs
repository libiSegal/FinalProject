
namespace BL.DataImplementation.ServiceInterfaces;
public interface IUserService :IDataService<UserDTO>
{
    UserDTO MapUser_UserDTO(User user);
    User MapUserDTO_User(UserDTO userDTO);
    Task<List<UserDTO>> GetAll(string managerId);
    Task<UserDTO> GetObject(string name, string password);

}