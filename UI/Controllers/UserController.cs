
using UI.Modules;

namespace Ui.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<IActionResult> Post(UserDTO user)
        {
            return Ok(await _userService.CreateObject(user));
        }
        [HttpPost("signIn")]
        public async Task<IActionResult> GetByNameAndPassword([FromBody]Client client)
        {
            return Ok(await _userService.GetObject(client.Name, client.Password));
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _userService.GetObject(id));
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UserDTO user)
        {
            return Ok(await _userService.UpdateObject(user));
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _userService.DeleteObject(id));
        }
    }
}