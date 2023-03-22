

using Dal;

namespace BL
{
    public interface IWashAbleService : IDataService<WashAbleDTO>
    {
        WashAbleDTO MapWashAble_washAbleDTO(WashAble washAble);
        WashAble MapWashAbleDTO_washAble(WashAbleDTO washAbleDTO);
        Task<List<WashAbleDTO>> GetAll(string userId);
        List<string> GetWashAblesId(List<WashAbleDTO> washAbles);
    }
}
