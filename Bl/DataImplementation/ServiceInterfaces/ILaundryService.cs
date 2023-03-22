
namespace BL.DataImplementation.ServiceInterfaces;

public interface ILaundryService : IDataService<LaundryDTO>
{
    Task<List<LaundryDTO>> GetAll(string managerId);
    public Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO);
    public LaundryDTO MapLaundry_LaundryDTO(Laundry laundry);

}