using AutoMapper;
using BL;
using Dal;


namespace Bl.Profiles;

public class ManagerProfile : Profile
{
    public ManagerProfile()
    {
        CreateMap<Manager, ManagerDTO>();
        CreateMap<User, UserDTO>();
    }
}
