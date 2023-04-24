using Microsoft.AspNetCore.Mvc;
using Bl;
using BL.DTO;
using Bl.Algorithm;
using BL.DataImplementation.ServiceInterfaces;
using Dal.DataExtensions;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public SchedulerController(IManagerService managerService)
        {
            _managerService = managerService;
        }
        // GET: api/<SchedulerController>
        [HttpGet]
       /* public List<WashAbleDTO> Get()
        {
            
        }*/

        // GET api/<SchedulerController>/5
        [HttpGet("{id}")]
        public Dictionary<DateTime, Dictionary<string, Category>> Get(string id)
        {
            Dictionary<DateTime, Dictionary<string, Category>> datesDict = new Dictionary<DateTime, Dictionary<string, Category>>()
         {
             {DateTime.Now , new Dictionary<string, Category>(){ {"a", Category.daily }  } },
             {new DateTime(2023,04,24,20,50,0) , new Dictionary<string, Category>(){ {"a", Category.daily }  } },
             {new DateTime(2023,04,26,20,50,0) , new Dictionary<string, Category>(){ {"a", Category.daily }  } },
             {new DateTime(2023,04,24,22,2,0) , new Dictionary<string, Category>(){ {"a", Category.daily }  } }
         };
           // ManagerDTO m =  _managerService.GetObject(id).Result;
            CalendarHandler s = new();
            return s.DatesLessThan24Hours(datesDict);

        }
    

        // POST api/<SchedulerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {

        }

        // PUT api/<SchedulerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SchedulerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
