using AutoMapper;
using BL;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl
{
    public class ManagerDTOManagementProfile :Profile
    {
        public ManagerDTOManagementProfile()
        {
            CreateMap<ManagerDTO, Manager>()
                           .ForMember(dest => dest.ItemsId,
                           opt => opt.MapFrom(src => src.Items.Select(i => i.ID)))
                           .ForMember(dest => dest.UsersID,
                           opt => opt.MapFrom(src => src.UsersDTO.Select(i => i.ID)))
                           .ForMember(dest => dest.LaundriesID,
                           opt => opt.MapFrom(src => src.LaundriesDTO.Select(i => i.ID)))
                           .ForMember(dest => dest.WashingMachine,
                           opt => opt.MapFrom(src => MapWashingMachine(src.WashingMachineDTO)))
                            .ForMember(dest => dest.Calendar,
                           opt => opt.MapFrom(src => MapCaledar(src.CalendarDTO)));

        }
        public WashingMachine MapWashingMachine(WashingMachineDTO src)
        { 
            return new MapperConfiguration(cfg => cfg.CreateMap<WashingMachineDTO, WashingMachine>())
                    .CreateMapper().Map<WashingMachine>(src);
        }
        public Calendar MapCaledar(CalendarDTO src)
        {
            return new MapperConfiguration(cfg => cfg.CreateMap<CalendarDTO, Calendar>())
                    .CreateMapper().Map<Calendar>(src);
        }
    }
}
