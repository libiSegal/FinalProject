
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
    
    }
}
