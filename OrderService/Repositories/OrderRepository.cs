using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly HashSet<Order> _orders = new HashSet<Order>();

        public Task Add(Order order)
        {
            if (order == null) throw new ArgumentNullException(nameof(order));
            order.Id = Guid.NewGuid().GetHashCode();
            _orders.Add(order);
            return Task.CompletedTask;
        }

        public IEnumerable<Order> GetAll()
        {
            return _orders.ToList();
        }
    }
}