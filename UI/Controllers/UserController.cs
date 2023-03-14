using BL;

using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public async Task<string> Post(UserDTO user)
        {
            return await _userService.CreateObject(user);
        }
        [HttpGet("{name}/{password}")]
        public Task<UserDTO> Get(string name, string password)
        {
            return _userService.GetObject(name, password);
        }
        [HttpGet]
        public Task<List<UserDTO>> GetAll(string managerId)
        {
            return _userService.GetAllUsers(managerId);
        }
        [HttpPut("{id}")]
        public Task<bool> Put([FromBody] UserDTO user, string id)
        {
            return _userService.UpdateObject(user,id);
        }
        [HttpDelete]
        public Task<bool> Delete(string id)
        {
            return _userService.DeleteObject(id);
        }

    }
}