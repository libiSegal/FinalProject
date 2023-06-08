
namespace UI.Controllers
{ 
    [ApiController]
    [Route("api/[controller]")]
    public class WashAbleController : ControllerBase
    {
        readonly IWashAbleService _washAbleService;
        public WashAbleController(IWashAbleService washAbleService)
        {
            _washAbleService = washAbleService;
        }

        [HttpGet("{id}")]
        public   async Task<IActionResult> Get(string id)
        {
            return Ok(await _washAbleService.GetObject(id));
        }

        [HttpGet()]
        public async Task<IActionResult> GetAll(string userId)
        {
            return Ok(await _washAbleService.GetAll(userId));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] WashAbleDTO washAbleDTO)
        {
          return Ok(await _washAbleService.CreateObject(washAbleDTO));
        }

        [HttpPut()]
        public async Task<IActionResult> Put([FromBody] WashAbleDTO washAbleDTO)
        {
            return Ok(await _washAbleService.UpdateObject(washAbleDTO));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _washAbleService.DeleteObject(id));
        }
    }
}
