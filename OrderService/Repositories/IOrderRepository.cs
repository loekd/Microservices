using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);

        Task<IEnumerable<Order>> GetAll();

        Task<Order> Find(int id);
    }
}
