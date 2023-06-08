
namespace BL.Profiles;
public class WashAbleProfile : Profile
{
    /// <summary>
    /// Map the washAble module
    /// </summary>
    public WashAbleProfile() => 
        CreateMap<WashAble, WashAbleDTO>().ReverseMap();

}