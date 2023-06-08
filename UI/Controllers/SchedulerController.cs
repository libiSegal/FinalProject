
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


        [HttpGet("{managerId}")]
        public IActionResult Get(string managerId)
        {

          ManagerDTO mananger =  _managerService.GetObject(managerId).Result;
          return Ok(_schedulerService.Scheduler(mananger));
        }
    
    }
}
