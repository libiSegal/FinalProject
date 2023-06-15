
namespace Dal.DataApi;
public interface IDataCRUD<T> where T : IDataBaseObject
{
    Task<T> ReadAsync(string id);    
    Task<bool> DeleteAsync(string id);
    Task<bool> UpdateAsync(T dataObject);
    Task<string> CreateAsync(T dataObject);
}
    
    
   