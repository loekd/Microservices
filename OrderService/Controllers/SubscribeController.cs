using System;
using System.Threading.Tasks;
using EventTypes;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class SubscribeController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public SubscribeController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpPost("~/api/[controller]/[action]")]
        public async Task<IActionResult> CustomerCreatedEvent([FromBody]CustomerCreatedEvent @event)
        {
            await _customerRepository.Add(new Customer
            {
                Id = @event.Id,
                Name = @event.Name
            }).ConfigureAwait(true);
            return Ok();
        }
    }
}
