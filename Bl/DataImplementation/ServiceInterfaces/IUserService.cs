
using Dal;

namespace BL
{
    public interface IUserService : IDataService<UserDTO>
    {
        Task<List<UserDTO>> GetAll(string managerId);
        Task<UserDTO> GetObject(string name, string password);
        //Task<List<string>> UpdateUsersList(ManagerDTO managerDTO, Manager managerFromDB);
    }
}