using Dal;

namespace BL
{
    public interface ILaundryService : IDataService<LaundryDTO>
    {
        Laundry ConvertLaundryDTOToLaundry(LaundryDTO laundryDTO);
        LaundryDTO ConvertLaundryToLaundryDTO(Laundry laundry);
        Task<List<LaundryDTO>> GetAll(string managerId);
       
    }
}