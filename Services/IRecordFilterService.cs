using RecordShop.Models;

namespace RecordShop.Services
{
    public interface IRecordFilterService
    {
        Task<int> RecordNameCount(string value);

        Task MigrateData();

        Task AddStockId();

        Task<int> RecordFormatCount(string value);
       }
}