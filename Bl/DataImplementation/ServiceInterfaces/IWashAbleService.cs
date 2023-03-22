
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public interface IWashAbleService : IDataService<WashAbleDTO>
    {
        WashAble MapWashAbleDTO_WashAble(WashAbleDTO washAbleDTO);
        WashAbleDTO MapWashAble_WashAbleDTO(WashAble washAble);
        Task<List<WashAbleDTO>> GetAll(string userId);
        List<string> GetWashAblesId(List<WashAbleDTO> washAbles);
    }
}
