
namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly ISchedulerService _schedulerService;
        public SchedulerController(IManagerService managerService , ISchedulerService schedulerService)
        {
            _managerService = managerService;
            _schedulerService = schedulerService;

        }


        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {

          ManagerDTO mananger =  _managerService.GetObject(id).Result;
          return Ok(_schedulerService.Scheduler(mananger));
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
