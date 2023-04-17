
namespace Bl.Profiles;

public class ManagerDTOProfile : Profile
{
    public ManagerDTOProfile()
    {
        CreateMap<ManagerDTO, Manager>();
        CreateMap<CalendarDTO, Calendar>();
        CreateMap<WashingMachineDTO, WashingMachine>();

    }

}

