
namespace Dal.DataImplementation.CRUDInterfaces;
public interface IManagerCRUD : IDataCRUD<Manager>
{
    Task<Manager> ReadAsync(string name, string password);
}