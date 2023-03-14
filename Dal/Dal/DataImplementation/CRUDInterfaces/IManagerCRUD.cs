
namespace Dal
{
    public interface IManagerCRUD : IDataCRUD<Manager>
    {
        Task<Manager> ReadAsync(string password, string Name);
    }
}