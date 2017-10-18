using EventTypes;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        [HttpPost("~/api/[controller]/[action]")]
        public IActionResult CustomerCreatedEvent([FromBody]CustomerCreatedEvent @event)
        {

            return Ok();
        }
    }
}
