using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserManagementProfile : Profile
    {
        /*public UserManagementProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Items,
                opt => opt.MapFrom(src => washAbleAction.GetAll(src.Id)));
        }*/
        public UserManagementProfile()
        {
            CreateMap<User, UserDTO>()
                .ForMember(dest => dest.Items,
                opt => opt.MapFrom<YourResolver, List<string>>(opt => opt.ItemsId));
        }
    }
    public class YourResolver : IMemberValueResolver<User, UserDTO, List<string>, List<WashAbleDTO>>
    {
        IWashAbleService WashAbleAction { get; }


        public YourResolver(IWashAbleService washAbleAction)
        {
            WashAbleAction = washAbleAction;
        }

     

        public List<WashAbleDTO> Resolve(User source, UserDTO destination, List<string> sourceMember, List<WashAbleDTO> destMember, ResolutionContext context)
        {
            return new List<WashAbleDTO>() { new WashAbleDTO("dress", "dfhsd", Colors.colourful, Status.clean, Category.daily, 3, 8) };
        }
    }
}
