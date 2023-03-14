using AutoMapper;
using BL;
using Dal;

namespace Bl
{
    public class ManagerManagementProfile : Profile
        //kdsfksjedhfjhsn
    {
        public ManagerManagementProfile()
        {
            CreateMap<Manager, ManagerDTO>()
                          .ForMember(dest => dest.WashingMachineDTO,
                          opt => opt.MapFrom(src => MapWashingMachine_WashingMachineDTO(src.WashingMachine)))
                          .ForMember(dest => dest.CalendarDTO,
                          opt => opt.MapFrom(src => MapCaledar_CalanderDTO(src.Calendar)));
        }
        public WashingMachineDTO MapWashingMachine_WashingMachineDTO(WashingMachine src)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<WashingMachine, WashingMachineDTO>())
                    .CreateMapper().Map<WashingMachineDTO>(src);
        }
        public CalendarDTO MapCaledar_CalanderDTO(Calendar src)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<Calendar, CalendarDTO>())
                    .CreateMapper().Map<CalendarDTO>(src);
        }
    }
}
