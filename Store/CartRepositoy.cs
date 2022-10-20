using MongoDB.Driver;
using RecordShop.Models;

namespace RecordShop.Store
{
    public class CartRepository : ICartRepository
    {
        private readonly IMongoCollection<Cart> _cartCollection;

        
        public CartRepository(IMongoDatabase mongoDatabase)
        {
            _cartCollection = mongoDatabase.GetCollection<Cart>("Cart");
        }
        
        public async Task<List<Cart>> GetCartAsync()
        {
            var result = await _cartCollection.Find(_ => true).ToListAsync();

            return result;
        }
        
        public async Task<Cart> GetCartByIdAsync(string id)
        {
            return await _cartCollection.Find(_ => _.Id == id).FirstOrDefaultAsync();
        }

        public async Task UpdateCartAsync(Cart cartToUpdate)
        {
            await _cartCollection.ReplaceOneAsync<Cart>(x => x.Id == cartToUpdate.Id, cartToUpdate);
        }
        
        public async Task CreateNewCartAsync(Cart newCart)
        {
            await _cartCollection.InsertOneAsync(newCart);
        }
        
        public async Task DeleteCartAsync(string id)
        {
            await _cartCollection.DeleteOneAsync(x => x.Id == id);
        }

    }
}
