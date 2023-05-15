
namespace BL.Profiles;
public class WashAbleProfile : Profile
{
    public WashAbleProfile()
    {
        CreateMap<WashAble, WashAbleDTO>().ReverseMap();
    }
}