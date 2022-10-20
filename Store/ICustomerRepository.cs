using RecordShop.Models;

namespace RecordShop.Store
{
    public interface ICustomerRepository
    {
        Task<List<Customer>> GetCustomerAsync();

        Task<Customer> GetCustomerByIdAsync(string id);

        Task UpdateCustomerAsync(Customer customerToUpdate);

        Task CreateNewCustomerAsync(Customer newCustomer);

        Task DeleteCustomerAsync(string id);

    }
}
