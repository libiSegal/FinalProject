
namespace Bl.Profiles;

public class ManagerDTOProfile : Profile
{
    public ManagerDTOProfile()
    {
        CreateMap<ManagerDTO, Manager>();  
        CreateMap<UserDTO, User>();
        CreateMap<LaundryDTO, Laundry>();
        CreateMap<CalendarDTO, Calendar>().ReverseMap();
    }
}

