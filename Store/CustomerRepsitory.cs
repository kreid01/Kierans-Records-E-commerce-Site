using MongoDB.Driver;
using RecordShop.Models;

namespace RecordShop.Store
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly IMongoCollection<Customer> _customerCollection;


        public CustomerRepository(IMongoDatabase mongoDatabase)
        {
            _customerCollection = mongoDatabase.GetCollection<Customer>("Customer");
        }

        public async Task<List<Customer>> GetCustomerAsync()
        {
            var result = await _customerCollection.Find(_ => true).ToListAsync();

            return result;
        }

        public async Task<Customer> GetCustomerByIdAsync(string linkToken)
        {
            return await _customerCollection.Find(_ => _.LinkToken == linkToken).FirstOrDefaultAsync();
        }

        public async Task UpdateCustomerAsync(Customer customerToUpdate)
        {
            await _customerCollection.ReplaceOneAsync<Customer>(x => x.LinkToken == customerToUpdate.LinkToken, customerToUpdate);
        }

        public async Task CreateNewCustomerAsync(Customer newCustomer)
        {
            await _customerCollection.InsertOneAsync(newCustomer);
        }

        public async Task DeleteCustomerAsync(string id)
        {
            await _customerCollection.DeleteOneAsync(x => x.Id == id);
        }

    }
}
