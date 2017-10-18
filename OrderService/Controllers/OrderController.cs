using System;
using System.Threading.Tasks;
using EventTypes;
using Microsoft.AspNetCore.Mvc;
using PubSub;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly IEventPublisher _pubSubServiceHelper;

        public OrderController(IEventPublisher pubSubServiceHelper)
        {
            _pubSubServiceHelper = pubSubServiceHelper ?? throw new ArgumentNullException(nameof(pubSubServiceHelper));
        }
        
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            var order = new OrderCreatedEvent
            {
                Product = "Bicycle",
                Quantity = 100
            };

            await _pubSubServiceHelper.Publish(order).ConfigureAwait(true);
            return Ok();
        }
    }
}
