
namespace Bl.Profiles;
public class LaundryProfile : Profile
{
    public LaundryProfile()
    {
        CreateMap<Laundry , LaundryDTO>().ReverseMap();

    }
}

