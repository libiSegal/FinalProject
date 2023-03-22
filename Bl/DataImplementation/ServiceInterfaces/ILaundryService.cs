using Dal;

namespace BL
{
    public interface ILaundryService : IDataService<LaundryDTO>
    {

        Laundry MapLaundryDTO_Laundry(LaundryDTO laundryDTO);
        LaundryDTO MapLaundry_LaundryDTO(Laundry laundry);
        Task<List<LaundryDTO>> GetAll(string managerId);
        //Task<List<string>> UpdateLaudryList(ManagerDTO managerDTO, Manager managerFromDB);

    }
}