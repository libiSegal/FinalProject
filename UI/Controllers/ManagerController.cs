
using UI.Modules;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        private readonly ILogger<ManagerController> _logger;
        public ManagerController(IManagerService managerService, ILogger<ManagerController> logger)
        {
            _managerService = managerService;
            _logger = logger;
        }
        [HttpPost]
        public async Task<IActionResult> CreateManager(ManagerDTO managerDTO)
        {
          //  _logger.LogDebug("hghgjh");
            return Ok(await _managerService.CreateObject(managerDTO));
        }
        [HttpGet("{id}")]
        public  async Task<IActionResult> Get(string id)
        {
            return Ok(await _managerService.GetObject(id)) ;
        }

        [HttpPost("signIn")]
        public  async Task<IActionResult> GetByNameAndPassword(Client client)
        {
            return Ok(await _managerService.GetObject(client.Name, client.Password));
        }

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(string id)
        {
            return  Ok(await _managerService.DeleteObject(id));
        }
        [HttpPut]
        public  async Task<IActionResult> Put(ManagerDTO managerDTO)
        {
            return  Ok(await _managerService.UpdateObject(managerDTO));
        }
    }
}
