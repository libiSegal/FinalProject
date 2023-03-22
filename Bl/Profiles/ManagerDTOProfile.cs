using AutoMapper;
using BL;
using Dal;


namespace Bl
{
    public class ManagerDTOProfile :Profile
    {
        public ManagerDTOProfile()
        {
            CreateMap<ManagerDTO, Manager>();
            CreateMap<CalendarDTO, Calendar>().ReverseMap();
            CreateMap<WashingMachineDTO, WashingMachine>().ReverseMap();

        }
     
    }
}
