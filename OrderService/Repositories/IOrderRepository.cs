using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);

        IEnumerable<Order> GetAll();
    }
}
