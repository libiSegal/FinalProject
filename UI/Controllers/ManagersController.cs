
using UI.Modules;

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;

        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _managerService.GetObject(id));
        }

        [HttpPost]
        public async Task<IActionResult> CreateManager(ManagerDTO managerDTO)
        {

            return Ok(await _managerService.CreateObject(managerDTO));
        }

        [HttpPut]
        public async Task<IActionResult> Put(ManagerDTO managerDTO)
        {
            return Ok(await _managerService.UpdateObject(managerDTO));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _managerService.DeleteObject(id));
        }

    }
}
