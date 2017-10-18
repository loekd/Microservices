using EventTypes;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class SubscribeController : Controller
    {
        [HttpPost("~/api/[controller]/[action]")]
        public IActionResult OrderCreatedEvent([FromBody]OrderCreatedEvent @event)
        {

            return Ok();
        }
    }
}
