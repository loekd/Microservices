using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventTypes;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly IPubSubServiceHelper _pubSubServiceHelper;

        public ValuesController(IPubSubServiceHelper pubSubServiceHelper)
        {
            _pubSubServiceHelper = pubSubServiceHelper ?? throw new ArgumentNullException(nameof(pubSubServiceHelper));
        }

        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]string value)
        {
            var order = new OrderCreated
            {
                Product = "Bicycle",
                Quantity = 100
            };

            var result = await _pubSubServiceHelper.PublishEvent(order).ConfigureAwait(true);
            result.EnsureSuccessStatusCode();

            return Ok();
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
