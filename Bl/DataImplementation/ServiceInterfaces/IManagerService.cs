using Dal;

namespace BL
{
    public interface IManagerService : IDataService<ManagerDTO>
    {
        Manager MapManagerDTO_Manager(ManagerDTO managerDTO);
        Task<ManagerDTO> MapManager_ManagerDTO(Manager manager);
        Task<ManagerDTO> GetObject(string name, string password);
    }
}