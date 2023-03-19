using AutoMapper;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class UserDTOProfile : Profile
    {
        public UserDTOProfile()
        {
            CreateMap<UserDTO,User>()
                .ForMember(dest => dest.ItemsId,
                opt => opt.MapFrom(src => src.Items.Select(i => i.ID)));
        }
    }
}
