using MongoDB.Driver;
using RecordShop.Models;

namespace RecordShop.Store
{
    public class OrderRepository : IOrderRepository
    {
            private readonly IMongoCollection<Order> _ordersCollection;


            public OrderRepository(IMongoDatabase mongoDatabase)
            {
                _ordersCollection = mongoDatabase.GetCollection<Order>("Orders");
            }

            public async Task<List<Order>> GetOrderAsync()
            {
                var result = await _ordersCollection.Find(_ => true).ToListAsync();

                return result;
            }

            public async Task<Order> GetOrderByIdAsync(string id)
            {
                return await _ordersCollection.Find(_ => _.Id == id).FirstOrDefaultAsync();
            }
            public async Task<List<Order>> GetAllOrdersByIdAsync(string CustomerLinkToken)
            {
                var result = await _ordersCollection.Find(_ =>_.CustomerLinkToken == CustomerLinkToken).ToListAsync();

                return result;
            }

        public async Task UpdateOrderAsync(Order orderToUpdate)
            {
                await _ordersCollection.ReplaceOneAsync<Order>(x => x.Id == orderToUpdate.Id, orderToUpdate);
            }

            public async Task CreateNewOrderAsync(Order newOrder)
            {
                await _ordersCollection.InsertOneAsync(newOrder);
            }

            public async Task DeleteOrderAsync(string id)
            {
                await _ordersCollection.DeleteOneAsync(x => x.Id == id);
            }

     }
}

