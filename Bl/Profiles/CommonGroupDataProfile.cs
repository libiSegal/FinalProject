

namespace BL.Profiles;

public class CommonGroupDataProfile : Profile
{
    public CommonGroupDataProfile()
    {
        CreateMap<CommonGroupDataDTO, CommonGroupData>().ReverseMap();
    }
}