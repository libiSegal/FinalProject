using AutoMapper;
using BL;
using Dal;

namespace Bl
{
    public class ManagerProfile : Profile    
    {
        private readonly IUserService _userService;        
        private readonly ILaundryService _laundryService;
        private readonly IWashAbleService _washAbleService;
        public ManagerProfile(IUserService userService , IWashAbleService washAbleService , ILaundryService laundryService )
        {
            _userService = userService;           
            _laundryService = laundryService;
            _washAbleService = washAbleService;

            CreateMap<Manager, ManagerDTO>()
                .ForMember(dest => dest.UsersDTO,
                opt => opt.MapFrom(src => _userService.GetAllUsers(src.ID).Result))
                .ForMember(dest => dest.LaundriesDTO,
                opt => opt.MapFrom(src => _laundryService.GetAll(src.ID).Result))
                .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => _washAbleService.GetAll(src.ID).Result));              
            CreateMap<Calendar, CalendarDTO>();
            CreateMap<WashingMachine, WashingMachineDTO>();

        }
    }
}
