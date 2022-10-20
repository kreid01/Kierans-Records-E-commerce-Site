using RecordShop.Models;

namespace RecordShop.Store
{
    public interface IOrderRepository
    {
        Task<List<Order>> GetOrderAsync();

        Task<Order> GetOrderByIdAsync(string id);

        Task UpdateOrderAsync(Order orderToUpdate);

        Task CreateNewOrderAsync(Order newOrder);

        Task DeleteOrderAsync(string id);

        Task<List<Order>> GetAllOrdersByIdAsync(string id);

    }
}
