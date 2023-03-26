
using Bl;
using BL;
using BL.DataImplementation.ServiceInterfaces;
using BL.DTO;
using Microsoft.AspNetCore.Mvc;

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
        public Task<string> Post(UserDTO user)
        {
            return  _userService.CreateObject(user);
        }
        [HttpGet("{name}/{password}")]
        public Task<UserDTO> Get(string name, string password)
        {
            return _userService.GetObject(name, password);
        }
        [HttpGet]
        public Task<List<UserDTO>> GetAll(string managerId)
        {
            return _userService.GetAll(managerId);
        }
        [HttpPut]
        public Task<bool> Put([FromBody] UserDTO user)
        {
            return _userService.UpdateObject(user);
        }
        [HttpDelete]
        public Task<bool> Delete(string id)
        {
            return _userService.DeleteObject(id);
        }

    }
}