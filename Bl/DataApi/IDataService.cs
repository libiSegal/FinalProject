
namespace BL.DataApi;
public interface IDataService<T> where T : IDataObject
{
    Task<string> CreateObject(T objectDTO);
    Task<T> GetObject(string id);
    Task<bool> DeleteObject(string id);
    Task<bool> UpdateObject(T objectBl);
}


