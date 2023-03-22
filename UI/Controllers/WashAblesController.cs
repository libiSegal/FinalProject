using BL;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WashAbleController : ControllerBase
    {
        readonly IWashAbleService _washAbleService;
        public WashAbleController(IWashAbleService washAbleService)
        {
            _washAbleService = washAbleService;
        }

        [HttpGet("user/{userId}/washable/{id}")]
        public  Task<WashAbleDTO> Get(string userId,string id)
        {
            return _washAbleService.GetObject(id) ;
        }

        [HttpGet("user/{userId}/washAbles")]
        public  Task<List<WashAbleDTO>> Get(string userId)
        {
            return _washAbleService.GetAll(userId);
        }

        [HttpPost]
        public  Task<string> Post([FromBody] WashAbleDTO washAbleDTO)
        {
          return  _washAbleService.CreateObject(washAbleDTO);
        }

        [HttpPut()]
        public  Task<bool> Put([FromBody] WashAbleDTO washAbleDTO)
        {
            return _washAbleService.UpdateObject(washAbleDTO);
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("user/{userId}/washAble/{id}")]
        public Task<bool> Delete(string id)
        {
            return _washAbleService.DeleteObject(id);
        }
    }
}
