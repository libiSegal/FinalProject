
namespace BL.DataImplementation.ServiceInterfaces;
public interface IWashAbleService : IDataService<WashAbleDTO>
{
    WashAbleDTO MapWashAble_washAbleDTO(WashAble washAble);
    WashAble MapWashAbleDTO_washAble(WashAbleDTO washAbleDTO);
    Task<List<WashAbleDTO>> GetAll(string userId);
    List<WashAbleDTO> GetWashAblesItems(List<string> washAbleIDs);
}
