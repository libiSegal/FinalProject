using AutoMapper;
using BL;
using Dal;


namespace Bl
{
    public class ManagerDTOProfile :Profile
    {
        public ManagerDTOProfile()
        {
            CreateMap<ManagerDTO, Manager>()
                           .ForMember(dest => dest.ItemsId,
                           opt => opt.MapFrom(src => src.Items.Select(i => i.ID)))
                           .ForMember(dest => dest.UsersID,
                           opt => opt.MapFrom(src => src.UsersDTO.Select(i => i.ID)))
                           .ForMember(dest => dest.LaundriesID,
                           opt => opt.MapFrom(src => src.LaundriesDTO.Select(i => i.ID)));
            CreateMap<CalendarDTO, Calendar>();
            CreateMap<WashingMachineDTO, WashingMachine>();

        }
     
    }
}
