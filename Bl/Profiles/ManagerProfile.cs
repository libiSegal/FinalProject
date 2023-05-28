
namespace BL.Profiles;
public class ManagerProfile : Profile
{
    public ManagerProfile()
    {
        CreateMap<ManagerDTO, Manager>().ReverseMap();  
        CreateMap<UserDTO, User>().ReverseMap();
        CreateMap<LaundryDTO, Laundry>().ReverseMap();
        CreateMap<CalendarDTO, Calendar>().ReverseMap();
    }
}