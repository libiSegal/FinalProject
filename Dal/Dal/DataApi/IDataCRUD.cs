
namespace Dal
{
    public interface IDataCRUD<T>where T : IDataBaseObject
    {
        Task<string> CreateAsync(T dataObject);
        Task<bool> UpdateAsync(T dataObject);
        Task<T> ReadAsync(string id);
        Task<bool> DeleteAsync(string id);
    }
}