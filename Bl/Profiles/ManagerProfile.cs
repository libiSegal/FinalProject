
namespace BL.Profiles;
public class ManagerProfile : Profile
{
    /// <summary>
    /// Map the manager module and the inner objects
    /// </summary>
    public ManagerProfile()
    {
        CreateMap<ManagerDTO, Manager>().ReverseMap();  
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<LaundryDTO, Laundry>().ReverseMap();
        CreateMap<CalendarDTO, Calendar>().ReverseMap();
    }
}