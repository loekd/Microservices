using System;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Models;
using CustomerService.Repositories;
using EventTypes;
using Microsoft.AspNetCore.Mvc;
using PubSub;

namespace CustomerService.Controllers.v3
{
    /// <summary>
    /// Example of a controller that can be reached by using /api/v3.0/customer
    /// </summary>
    [ApiVersion("3.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IEventPublisher _pubSubServiceHelper;

        public CustomerController(IEventPublisher pubSubServiceHelper, ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository ?? throw new ArgumentNullException(nameof(customerRepository));
            _pubSubServiceHelper = pubSubServiceHelper ?? throw new ArgumentNullException(nameof(pubSubServiceHelper));
        }
        
        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody]Customer customer)
        {
            if (customer == null) return BadRequest();

            //persist
            await _customerRepository.Add(customer).ConfigureAwait(true);

            //notify
            var customerCreatedEvent = new CustomerCreatedEvent
            {
                Name = customer.Name,
                Id = customer.Id
            };

            await _pubSubServiceHelper.Publish(customerCreatedEvent).ConfigureAwait(true);

            //return created object
            return Created($"/api/customer/{customer.Id}", customer);
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = await _customerRepository.GetAll().ConfigureAwait(true);
            return Ok(result);
        }

        [HttpGet, MapToApiVersion("3.1")]
        public async Task<IActionResult> GetV3_1()
        {
            var result = await _customerRepository.GetAll().ConfigureAwait(true);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var result = await _customerRepository
                .Find(id)
                .ConfigureAwait(true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        [HttpGet("{customerId}/orders")]
        public async Task<IActionResult> GetCustomerOrders(int customerId)
        {
            var result = await _customerRepository
                .Find(customerId)
                .ConfigureAwait(true);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result.Orders);
        }

        [HttpGet("version")]
        public IActionResult GetVersion()
        {
            var version = GetType().GetCustomAttributes(typeof(ApiVersionAttribute), false).Cast<ApiVersionAttribute>().Single()
                .Versions.First();
            return Ok($"{version.MajorVersion}.{version.MinorVersion}");
        }
    }
}
