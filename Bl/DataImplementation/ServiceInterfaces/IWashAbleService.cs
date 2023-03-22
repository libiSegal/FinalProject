
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
        /*Task<string> CreateObject(WashAbleDTO objectDTO);*/
        WashAble ConvertWashAbleDTOToWashAble(WashAbleDTO washAbleDTO);
        WashAbleDTO ConvertWashAbleToWashAbleDTO(WashAble washAble);
        Task<List<WashAbleDTO>> GetAll(string userId);
        List<string> GetWashAblesId(List<WashAbleDTO> washAbles);
    }
}
