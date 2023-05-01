
namespace BL.DTO;

public class ManagerDTO : UserDTO
{

    public List<UserDTO> UsersDTO { get; set; }
    public List<LaundryDTO> LaundriesDTO { get; set; }
    public CalendarDTO Calendar { get; set; }
    public WashingMachineDTO WashingMachineDTO { get; set; }
    public int SpairDays { get; set; }
    public List<string> WashAblesCollectionTypes { get; set; }

    public ManagerDTO() : base()
    {
        UsersDTO = new();
        LaundriesDTO = new();
        WashingMachineDTO = new();
        WashAblesCollectionTypes = new();
        Calendar = new();
    }

    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine) : base(name, password, "")
    {
        WashingMachineDTO = washingMachine;
        UsersDTO = new();
        LaundriesDTO = new();
        ActionPermissions = ActionPermission.a | ActionPermission.b;
        WashAblesCollectionTypes = new();
        Calendar = new();
    }
    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine, CalendarDTO calendar, int spareDays = 1) : this(name, password, washingMachine)
    { 
        Calendar = calendar;
        SpairDays = spareDays;
    }
    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine, CalendarDTO calendar, int spareDays, List<UserDTO> users, List<LaundryDTO> laundries) : this(name, password, washingMachine, calendar, spareDays)
    {
        UsersDTO = users;
        LaundriesDTO = laundries;  
    }
}
