using Microsoft.AspNetCore.Mvc;
using Bl;
using BL.DTO;
using Bl.Algorithm;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace UI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SchedulerController : ControllerBase
    {
        // GET: api/<SchedulerController>
        [HttpGet]
        public List<WashAbleDTO> Get()
        {
            SchedulerService schedulerService = new SchedulerService();
            return schedulerService.fill();
        }

        // GET api/<SchedulerController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SchedulerController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SchedulerController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SchedulerController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
