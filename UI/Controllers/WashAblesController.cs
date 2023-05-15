
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

        [HttpGet("user/{userId}/washable/{id}")]
        public  async Task<IActionResult> Get(string userId,string id)
        {
            return Ok(await _washAbleService.GetObject(id) ) ;
        }

        [HttpGet("user/{userId}/washAbles")]
        public async Task<IActionResult> Get(string userId)
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

        [HttpDelete("user/{userId}/washAble/{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            return Ok(await _washAbleService.DeleteObject(id));
        }
    }
}
