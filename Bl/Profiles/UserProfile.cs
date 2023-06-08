
namespace BL.Profiles;
public class UserProfile : Profile
{
    /// <summary>
    /// Map the user module
    /// </summary>
    public UserProfile() =>
        CreateMap<User, UserDTO>().ReverseMap();

}

