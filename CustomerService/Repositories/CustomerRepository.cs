using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HashSet<Customer> _customers = new HashSet<Customer>();

        public Task Add(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            customer.Id = Guid.NewGuid().GetHashCode();
            _customers.Add(customer);
            return Task.CompletedTask;
        }

        

        public Task<Customer> Find(int id)
        {
            return Task.FromResult(_customers.SingleOrDefault(c => c.Id == id));
        }

        public Task<IEnumerable<Customer>> GetAll()
        {
            return Task.FromResult(_customers.ToList().AsEnumerable());
        }

        public async Task Add(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));

            var customer = await Find(order.CustomerId).ConfigureAwait(false);
            if (customer == null) throw new InvalidOperationException($"Cannot add order to customer '{order.CustomerId}', customer not found.");

            customer.Orders.Add(order);
        }
    }
}