﻿
using UI.Modules;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagersController : ControllerBase
    {
        private readonly IManagerService _managerService;
       
        public ManagersController(IManagerService managerService)
        {
            _managerService = managerService;
        }
        [HttpPost]
        public async Task<IActionResult> CreateManager(ManagerDTO managerDTO)
        {

            return Ok(await _managerService.CreateObject(managerDTO));
        }
        [HttpGet("{id}")]
        public  async Task<IActionResult> Get(string id)
        {
            return Ok(await _managerService.GetObject(id)) ;
        }
        

        [HttpDelete("{id}")]
        public  async Task<IActionResult> Delete(string id)
        {
            return Ok(await _managerService.DeleteObject(id));
        }
        [HttpPut]
        public  async Task<IActionResult> Put(ManagerDTO managerDTO)
        {
            return Ok(await _managerService.UpdateObject(managerDTO));
        }
    }
}