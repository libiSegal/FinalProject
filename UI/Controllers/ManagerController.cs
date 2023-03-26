using BL;
using BL.DataImplementation.ServiceInterfaces;
using BL.DTO;
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
        public Task<string> CreateManager(ManagerDTO managerDTO)
        {
            return  _managerService.CreateObject(managerDTO);
        }
        [HttpGet("{id}")]
        public  Task<ManagerDTO> Get(string id)
        {
            return  _managerService.GetObject(id);
        }

        [HttpGet("{name}/{password}")]
        public  Task<ManagerDTO> Get(string name, string password)
        {
            return  _managerService.GetObject(name, password);
        }

        [HttpDelete("{id}")]
        public  Task<bool> Delete(string id)
        {
            return  _managerService.DeleteObject(id);
        }
        [HttpPut]
        public  Task<bool> Put(ManagerDTO managerDTO)
        {
            return  _managerService.UpdateObject(managerDTO);
        }
    }
}
