using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.Repositories
{
    public interface IOrderRepository
    {
        Task Add(Order order);

        IEnumerable<Order> GetAll();
    }
}
