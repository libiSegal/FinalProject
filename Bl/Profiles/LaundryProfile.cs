using AutoMapper;
using BL;
using Dal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bl.Profiles;
public class LaundryProfile : Profile
{
    public LaundryProfile()
    {
        CreateMap<Laundry , LaundryDTO>().ReverseMap();

    }
}

