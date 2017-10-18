using EventTypes;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        [HttpPost("~/api/[controller]/[action]")]
        public IActionResult OrderCreatedEvent([FromBody]OrderCreatedEvent @event)
        {

            return Ok();
        }
    }
}
