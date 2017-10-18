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

        public Task<IEnumerable<Order>> GetAll()
        {
            return Task.FromResult(_orders.ToList().AsEnumerable());
        }

        public Task<Order> Find(int id)
        {
            var result = _orders.SingleOrDefault(o => o.Id == id);
            return Task.FromResult(result);
        }
    }
}