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
        public async Task<WashAbleDTO> Get(string userId,string id)
        {
            return await _washAbleService.GetObject(id) ;
        }

        [HttpGet("user/{userId}/washAbles")]
        public async Task<List<WashAbleDTO>> Get(string userId)
        {
            return await _washAbleService.GetAll(userId);
        }

        // POST api/<ValuesController>
        [HttpPost]
        public async Task<string> Post([FromBody] WashAbleDTO washAbleDTO)
        {
          return await  _washAbleService.CreateObject(washAbleDTO);
        }

        // PUT api/<ValuesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<ValuesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
