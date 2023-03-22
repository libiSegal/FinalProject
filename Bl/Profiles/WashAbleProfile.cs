using AutoMapper;
using BL;
using Dal;

namespace Bl.Profiles;

public class WashAbleProfile : Profile
{
    public WashAbleProfile()
    {
        CreateMap<WashAble, WashAbleDTO>().ReverseMap();
    }
}

