using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerService.Models;

namespace CustomerService.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);

        Task<IEnumerable<Customer>> GetAll();

        Task<Customer> Find(int id);

        Task Add(Order order);
    }
}
