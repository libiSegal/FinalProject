
namespace Bl.Profiles;

public class ManagerDTOProfile : Profile
{
    public ManagerDTOProfile()
    {
        CreateMap<ManagerDTO, Manager>();  
        CreateMap<UserDTO, User>();
    //    CreateMap<CalendarDTO, Calendar>().ReverseMap();
        CreateMap<WashingMachineDTO, WashingMachine>().ReverseMap();

        CreateMap<CalendarDTO, Calendar>().ReverseMap();

    }

}

