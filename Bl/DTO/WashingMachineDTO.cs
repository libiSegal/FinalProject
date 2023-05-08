
namespace BL.DTO;

public class WashingMachineDTO
{

    public string Company { get; set; }
    public string Model { get; set; }
    public int LaundryWeight { get; set; }
    public List<Dictionary<string, string>> Programs { get; set; }

    public WashingMachineDTO()
    {
        Company = "XXX";
        LaundryWeight = 1;
        Model = "XXX";
        Programs = new();
    }
   /* public WashingMachineDTO(string company, string model, int laundryWeight)
    {
        Company = company;
        Model = model;
        LaundryWeight = laundryWeight;
        Programs = new();
    }
    public WashingMachineDTO(string company, string model, int laundryWeight, List<Dictionary<string, string>> programs)
    {
        Company = company;
        Model = model;
        LaundryWeight = laundryWeight;
        Programs = new(programs);
    }*/
}
