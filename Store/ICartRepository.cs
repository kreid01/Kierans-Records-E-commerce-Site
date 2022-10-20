using RecordShop.Models;

namespace RecordShop.Store
{
    public interface ICartRepository
    {
        Task<List<Cart>> GetCartAsync();

        Task<Cart> GetCartByIdAsync(string id);

        Task UpdateCartAsync(Cart cartToUpdate);

        Task CreateNewCartAsync(Cart newCart);

        Task DeleteCartAsync(string id);
    }
}
