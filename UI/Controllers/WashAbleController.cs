using BL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Ui.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WashAbleController : ControllerBase
    {
        /*public TestCreate TestCreate { get; set; }
        private readonly IWashAbleAction _washAbleAction;
        public WashAbleController(IWashAbleAction washableAction)
        {
            _washAbleAction = washableAction;
        }
        [HttpPost]
        public Task<bool> Post(WashAbleBl washAble)
        {
            return _washAbleAction.CreateObject(washAble);
        }
        [HttpGet("{id}")]
        public Task<WashAbleBl> Get(string id)
        {
            return _washAbleAction.GetObject(id);
        }
        [HttpGet]
        public Task<List<WashAbleBl>> GetAll(string userId)
        {
            return _washAbleAction.GetAllWashAbles(userId);
        }
        [HttpPut("{id}")]
        public Task<bool> Put([FromBody] WashAbleBl user, string id)
        {
            return _washAbleAction.UpdateObject(user, id);
               
        }
        [HttpDelete]
        public Task<bool> Delete(string id)
        {
            return _washAbleAction.DeleteObject(id);
        }
        [HttpPost]
        public Task<bool> PostLandry(LaundryBl washAble)
        {
            return TestCreate.CreateLaundry(washAble);
        }*/
    }
}
