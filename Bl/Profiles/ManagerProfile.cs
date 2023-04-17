
namespace Bl.Profiles;
public class ManagerProfile : Profile
{
    public ManagerProfile()
    {
        CreateMap<Manager, ManagerDTO>();
        CreateMap<User, UserDTO>();
        CreateMap<Calendar, CalendarDTO>();
        CreateMap<WashingMachine, WashingMachineDTO>();
    }
}
