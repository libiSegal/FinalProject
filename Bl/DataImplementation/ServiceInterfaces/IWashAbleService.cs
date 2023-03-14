
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
        /* Task<bool> CreateWashAble(WashAbleBl washAbleBl);
         Task<bool> DeleteWashAble(string id);*/
        Task<List<WashAbleDTO>> GetAll(string userId);
        /*Task<WashAbleBl> GetWashAble(string id);
        Task<bool> UpdateWashAble(WashAbleBl washAbleBl, string id);*/
        Task<List<string>> UpdateItemsList(UserDTO userDTO, User userFromDB);
        List<string> GetWashAblesId(List<WashAbleDTO> washAbles);
    }
}
