using System;
using System.Threading.Tasks;
using CustomerService.Models;
using CustomerService.Repositories;
using EventTypes;
using Microsoft.AspNetCore.Mvc;

namespace CustomerService.Controllers
{
    [Route("api/[controller]")]
    public class SubscriptionController : Controller
    {
        private readonly ICustomerRepository _customerRepository;

        public SubscribeController(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
        }

        [HttpPost("~/api/[controller]/[action]")]
        public async Task<IActionResult>  OrderCreatedEvent([FromBody]OrderCreatedEvent @event)
        {
            await _customerRepository.Add(new Order
            {
                Id = @event.Id,
                Product = @event.Product,
                Quantity = @event.Quantity,
                CustomerId = @event.CustomerId
            }).ConfigureAwait(true);
            return Ok();
        }
    }
}
