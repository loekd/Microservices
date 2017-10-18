using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventTypes;
using Microsoft.AspNetCore.Mvc;
using PubSub;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class CustomerController : Controller
    {
        private readonly IEventPublisher _pubSubServiceHelper;

        public CustomerController(IEventPublisher pubSubServiceHelper)
        {
            _pubSubServiceHelper = pubSubServiceHelper ?? throw new ArgumentNullException(nameof(pubSubServiceHelper));
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]string value)
        {
            var customerCreatedEvent = new CustomerCreatedEvent
            {
                Name = "Customer X",
                Id = new Random(131365).Next(0, 100)
            };

            await _pubSubServiceHelper.Publish(customerCreatedEvent).ConfigureAwait(true);
            return Ok();
        }
    }
}
