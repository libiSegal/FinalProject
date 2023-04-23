﻿
namespace BL.DTO;

public class ManagerDTO : UserDTO
{

    public CalendarDTO CalendarDTO { get; set; }
    public List<UserDTO> UsersDTO { get; set; }
    public List<LaundryDTO> LaundriesDTO { get; set; }
    public WashingMachineDTO WashingMachineDTO { get; set; }    
    public List<string> WashAblesCollectionTypes { get; set; }

    public ManagerDTO() : base()
    {
        UsersDTO = new();
        CalendarDTO = new();
        LaundriesDTO = new();
        WashingMachineDTO = new();
        WashAblesCollectionTypes = new();
    }

    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine) : base(name, password, "")
    {
        CalendarDTO = new();
        WashingMachineDTO = washingMachine;
        UsersDTO = new();
        LaundriesDTO = new();
        ActionPermissions = ActionPermission.a | ActionPermission.b;
        WashAblesCollectionTypes = new();
    }
    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine, CalendarDTO calendar):base(name, password, "")
    {                                 
        CalendarDTO = calendar;
        WashingMachineDTO = washingMachine;
        UsersDTO = new();
        LaundriesDTO = new();
        WashAblesCollectionTypes = new();
        ActionPermissions = ActionPermission.a | ActionPermission.b;
    }
    public ManagerDTO(string name, string password, WashingMachineDTO washingMachine, CalendarDTO calendar, List<UserDTO> users, List<LaundryDTO> laundries) : this(name, password, washingMachine, calendar)
    {
        UsersDTO = users;
        LaundriesDTO = laundries;
        WashAblesCollectionTypes = new();
    }
}
