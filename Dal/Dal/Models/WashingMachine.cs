
namespace Dal.Models;

public class WashingMachine
{
    public string Company { get; set; }
    public string Model { get; set; }
    public int LaundryWeight { get; set; }
    public List<Dictionary<string , string>> Programs { get; set; }
    public WashingMachine()
    {
        Company = "XXX";
        Model = "XXX";
        Programs = new();
    }
    public WashingMachine( string company ,string model ,int laundryWeight, List<Dictionary<string, string>> programs)
    {
        Company = company;
        Model = model;
        LaundryWeight = laundryWeight;
        Programs = new(programs);
    }
}
