
namespace BL.DTO;

public class ManagerDTO : UserDTO
{
    public CalendarDTO CalendarDTO { get; set; }
    public List<UserDTO> UsersDTO { get; set; }
    public List<LaundryDTO> LaundriesDTO { get; set; }
    public WashingMachineDTO WashingMachineDTO { get; set; }

    public ManagerDTO() : base()
    {
        UsersDTO = new();
        CalendarDTO = new();
        LaundriesDTO = new();
        WashingMachineDTO = new("XXX" ,"XXX" , new List<Dictionary<string, string>>()); 
    }
    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine, CalendarDTO calendar):base(name, password, "")
    {                                 
        CalendarDTO = calendar;
        WashingMachineDTO = washingMachine;
        UsersDTO = new();
        LaundriesDTO = new();
        ActionPermissions = ActionPermission.a | ActionPermission.b;
    }
    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine, CalendarDTO calendar, List<UserDTO> users, List<LaundryDTO> laundries) : this(name, password, washingMachine, calendar)
    {
        UsersDTO = users;
        LaundriesDTO = laundries;
    }
}
