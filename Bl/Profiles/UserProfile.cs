﻿
namespace Bl.Profiles;
public class UserProfile : Profile
{
    public UserProfile()
    {
        CreateMap<User, UserDTO>().ReverseMap();
    }
}

