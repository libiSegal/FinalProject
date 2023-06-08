

namespace BL.Profiles;

public class CommonGroupDataProfile : Profile
{
    /// <summary>
    /// Map the common group data module
    /// </summary>
    public CommonGroupDataProfile() =>
        CreateMap<CommonGroupDataDTO, CommonGroupData>().ReverseMap();

}