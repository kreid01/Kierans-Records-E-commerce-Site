using MongoDB.Driver;
using RecordShop.Store;

namespace RecordShop.Services
{
    public class RecordFilterService: IRecordFilterService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordFilterService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<int> RecordNameCount (string value)
        {
            var result = await _recordRepository.GetAllAsync();

            return result.Where(r => r.name == value && r.isAvailable == true).Count();
        }

        public async Task MigrateData()
        {
            var records = await _recordRepository.GetAllAsync();

            foreach (var record in records)
            {
                record.isAvailable = true;
                await _recordRepository.UpdateRecordAsync(record);
            }
        }
    }
}
