using RecordShop.Models;

namespace RecordShop.Store
{
    public interface IRecordRepository
    {
        Task<List<Record>> GetAllPagedAsync(RecordParameters recordParameters);

        Task<List<Record>> GetAllAsync();

        Task <Record> GetByStockNumberAsync(string stockNumber);

        Task<List<Record>> GetByGenre(string genre);

        Task <Record> GetByNameAsync(string name);
        Task CreateNewRecordAsync(Record newRecord);

        Task UpdateRecordAsync(Record recordToUpdate);

        Task DeleteRecordAsync(string id);

    }   
}