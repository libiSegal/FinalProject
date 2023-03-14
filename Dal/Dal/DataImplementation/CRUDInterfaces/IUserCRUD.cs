
namespace Dal
{
    public interface IUserCRUD : IDataCRUD<User>
    {
        Task<User> ReadAsync(string password, string Name);
        Task<List<User>> ReadAllAsync(string managerId);
        
    }
}