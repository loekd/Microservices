using System;
using System.Linq;
using System.Threading.Tasks;
using EventTypes;
using Microsoft.AspNetCore.Mvc;
using OrderService.Models;
using OrderService.Repositories;
using PubSub;

namespace OrderService.Controllers
{
    [Route("api/[controller]")]
    public class OrderController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IOrderRepository _orderRepository;
        private readonly IEventPublisher _pubSubServiceHelper;

        public OrderController(IEventPublisher pubSubServiceHelper, IOrderRepository orderRepository, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            _pubSubServiceHelper = pubSubServiceHelper ?? throw new ArgumentNullException(nameof(pubSubServiceHelper));
        }
        
        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            
            //validate
            var customer = _customerRepository.GetAll().SingleOrDefault(c => c.Id == order.CustomerId);
            if (customer == null)
            {
                ModelState.AddModelError(nameof(Order.CustomerId), $"Customer with ID {order.CustomerId} does not exist.");
                return BadRequest(ModelState);
            }
            //persist
            await _orderRepository.Add(order).ConfigureAwait(true);
            
            //publish
            var orderCreatedEvent = new OrderCreatedEvent
            {
                Id = order.Id,
                Product = order.Product,
                Quantity = order.Quantity,
                CustomerId = order.CustomerId
            };

            await _pubSubServiceHelper.Publish(orderCreatedEvent).ConfigureAwait(true);
            return Ok();
        }
    }
}
