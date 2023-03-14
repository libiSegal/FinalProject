using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class WashingMachineDTO
    {

        public string Company { get; set; }
        public string Model { get; set; }
        public int LaundryWeight { get; set; }
        public List<Dictionary<string, string>> Programs { get; set; }

      
        public WashingMachineDTO(string company, string model, List<Dictionary<string, string>> programs)
        {
            Company = company;
            Model = model;
            Programs = new(programs);
        }
    }
}
