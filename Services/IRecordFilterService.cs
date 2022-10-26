using RecordShop.Models;

namespace RecordShop.Services
{
    public interface IRecordFilterService
    {
        Task<int> RecordNameCount(string value);

        Task MigrateData();

        Task AddStockId();

        Task AddFormat();

        Task<int> RecordFormatCount(string value);

        Task<List<Record>> SortRecords(string value, List<Record> records);

        Task<List<Record>> RemoveDuplicateRecords(List<Record> records);
       }
}