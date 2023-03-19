using AutoMapper;
using Dal;

namespace BL
{
    public class UserProfile : Profile
    {
        private readonly IWashAbleService _washAbleService;
        public UserProfile(IWashAbleService washAbleService)
            
        {
            _washAbleService = washAbleService;
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Items,
                opt => opt.
                MapFrom(src => _washAbleService.GetAll(src.ID).Result));
              
        }
      
    }
}
  