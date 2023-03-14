using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ManagerController : ControllerBase
    {
        private readonly IManagerService _managerService;
        public ManagerController(IManagerService managerService)
        {
            _managerService = managerService;
        }
        [HttpPost]
        public async Task<string> CreateManager(ManagerDTO managerDTO)
        {
            return await _managerService.CreateObject(managerDTO);
        }
        [HttpGet("{id}")]
        public async Task<ManagerDTO> Get(string id)
        {
            return await _managerService.GetObject(id);
        }
    }
}
