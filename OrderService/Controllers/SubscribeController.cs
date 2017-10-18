using EventTypes;
using Microsoft.AspNetCore.Mvc;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class SubscribeController : Controller
    {
        [HttpPost("~/api/[controller]/[action]")]
        public IActionResult OrderCreated([FromBody]OrderCreated eventType)
        {

            return Ok();
        }
    }
}
