
using UI.Modules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SignInController : ControllerBase
    {

        private readonly IManagerService _managerService;
        private readonly IUserService _userService;
        public SignInController(IManagerService managerService , IUserService userService)
        {   
            _managerService = managerService;
            _userService = userService;
        }
        [HttpPost("user")]
        public async Task<IActionResult> GetUser([FromBody] Client client)
        {
            return Ok(await _userService.GetObject(client.Name, client.Password));
        }

        [HttpPost("manager")]
        public async Task<IActionResult> GetManager(Client client)
        {
            return Ok(await _managerService.GetObject(client.Name, client.Password));
        }


    }
}
