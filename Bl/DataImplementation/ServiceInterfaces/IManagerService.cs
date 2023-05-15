
namespace BL.DataImplementation.ServiceInterfaces;
public interface IManagerService : IDataService<ManagerDTO>
{
    Task<ManagerDTO> GetObject(string name, string password);
    Task<ManagerDTO> MapManager_ManagerDTO(Manager manager);
    Manager MapManagerDTO_Manager(ManagerDTO managerDTO);
}