
namespace BL.DataImplementation.ServiceInterfaces;
public interface ILaundryService : IDataService<LaundryDTO>
{
    Task<List<LaundryDTO>> GetAll(string managerId);
    Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO);
    LaundryDTO MapLaundry_LaundryDTO(Laundry laundry);
}