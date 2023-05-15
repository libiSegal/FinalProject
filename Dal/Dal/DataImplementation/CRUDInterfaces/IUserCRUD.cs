
namespace Dal.DataImplementation.CRUDInterfaces;
public interface IUserCRUD : IDataCRUD<User>
{
    Task<User> ReadAsync(string name, string password);
    Task<List<User>> ReadAllAsync(string managerId);
    
}