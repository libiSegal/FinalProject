using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IDataService<T> where T : IDataObject
    {
        Task<string> CreateObject(T objectDTO);
        Task<T> GetObject(string id);
        Task<bool> DeleteObject(string id);
        Task<bool> UpdateObject(T objectBl, string id);
    }
}
