
namespace BL.DTO;
public class ManagerDTO : UserDTO
{
    public List<UserDTO> UsersDTO { get; set; }
    public List<LaundryDTO> LaundriesDTO { get; set; }
    public CalendarDTO Calendar { get; set; }
    public int WashingMachineWeight { get;  set; }
  

    public ManagerDTO() : base()
    {
        UsersDTO = new();
        LaundriesDTO = new();
        WashingMachineWeight = 1;
        Calendar = new();
    }
}
