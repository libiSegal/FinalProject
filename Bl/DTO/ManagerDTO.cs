
namespace BL.DTO;
public class ManagerDTO : UserDTO
{
    public List<UserDTO> UsersDTO { get; set; }
    public List<LaundryDTO> LaundriesDTO { get; set; }
    public CalendarDTO Calendar { get; set; }
    public WashingMachineDTO WashingMachineDTO { get; set; }
    public List<string> WashAblesCollectionTypes { get; set; }

    public ManagerDTO() : base()
    {
        UsersDTO = new();
        LaundriesDTO = new();
        WashingMachineDTO = new();
        WashAblesCollectionTypes = new();
        Calendar = new();
    }
   /* public ManagerDTO(string name, string password, Gender gender, WashingMachineDTO washingMachine) : base(name, password, "", gender)
    {
        WashingMachineDTO = washingMachine;
        UsersDTO = new();
        LaundriesDTO = new();
        ActionPermissions = ActionPermission.a | ActionPermission.b;
        WashAblesCollectionTypes = new();
        Calendar = new();
    }
    public ManagerDTO(string name, string password, Gender gender, WashingMachineDTO washingMachine, CalendarDTO calendar) : this(name, password, gender, washingMachine)
    {
        Calendar = calendar;
    }
    public ManagerDTO(string name, string password, Gender gender, WashingMachineDTO washingMachine, CalendarDTO calendar, List<UserDTO> users, List<LaundryDTO> laundries) : this(name, password, gender, washingMachine, calendar)
    {
        Calendar = calendar;
        UsersDTO = users;
        LaundriesDTO = laundries;
    }*/
}
