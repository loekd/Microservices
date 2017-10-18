using System.Collections.Generic;
using System.Threading.Tasks;
using OrderService.Models;

namespace OrderService.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);

        IEnumerable<Customer> GetAll();

        Task<Customer> Find(int id);
    }
}
