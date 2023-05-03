using BL;
using BL.DataImplementation.ServiceInterfaces;
using BL.DTO;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
        public  Task<ManagerDTO> Get(string id)
        {
            return  _managerService.GetObject(id);
        }

        [HttpGet("{name}/{password}")]
        public  async Task<IActionResult> Get(string name, string password)
        {
            return Ok(await _managerService.GetObject(name, password));
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
