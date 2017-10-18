using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly HashSet<Customer> _customers = new HashSet<Customer>();

        public Task Add(Customer customer)
        {
            if (customer == null) throw new ArgumentNullException(nameof(customer));
            _customers.Add(customer);
            return Task.CompletedTask;
        }

        public IEnumerable<Customer> GetAll()
        {
            return _customers.ToList();
        }

        public Task<Customer> Find(int id)
        {
            return Task.FromResult(_customers.SingleOrDefault(c => c.Id == id));
        }
    }
}