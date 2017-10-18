using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HashSet<Order> _orders = new HashSet<Order>();

        public Task Add(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders.ToList();
        }
    }
}