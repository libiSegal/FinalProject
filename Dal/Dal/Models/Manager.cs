

namespace Dal
{
    public class Manager : User
    {
        
        public WashingMachine WashingMachine { get; set; }
        public Calendar Calendar { get; set; }
        public List<string> UsersID { get; set; }
        public List<string> LaundriesID { get; set; }
        public Manager() : base()
        {
            WashingMachine = new("XXX" , "XXX" , new());
            Calendar = new();
            UsersID = new();
            LaundriesID = new();

        }
        public Manager(string name, string password, WashingMachine washingMachine, Calendar calendar) :base(name, password)
        {
            /*ManagerID = "";*/
            Calendar = calendar;
            WashingMachine = washingMachine;
            UsersID = new();
            LaundriesID = new();
            ActionPermissions = ActionPermission.a | ActionPermission.b ; 
        }
        public Manager(string name, string password, WashingMachine washingMachine, Calendar calendar , List<string> usersId, List<string> laundriesId) : this(name, password, washingMachine, calendar)
        {
            UsersID = usersId;
            LaundriesID = laundriesId;
        }

    }
}
