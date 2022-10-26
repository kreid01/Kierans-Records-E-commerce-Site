using MongoDB.Driver;
using RecordShop.Models;

namespace RecordShop.Store
{
    public class RecordRepository : IRecordRepository
    {
        private readonly IMongoCollection<Record> _recordCollection;

        public RecordRepository(IMongoDatabase mongoDatabase)
        {
            _recordCollection = mongoDatabase.GetCollection<Record>("Records");
        }

        public async Task<List<Record>> GetAllPagedAsync(RecordParameters recordParameters)
        {
            var result = await _recordCollection
                .Find(_ => true)
                .Skip((recordParameters.PageNumber - 1) * recordParameters.PageSize)
                .Limit(recordParameters.PageSize)
                .ToListAsync();

            return result;
        }
        public async Task<List<Record>> GetAllAsync()
        {
            var result = await _recordCollection.Find(_ => true).ToListAsync();

            return result;
        }

        public async Task<Record> GetByStockNumberAsync(string stockNumber)
        {
            return await _recordCollection.Find(_ => _.stockNumber == stockNumber).FirstOrDefaultAsync();
        }

        public async Task<List<Record>> GetByGenre(string genreToFind, List<Record> records)
        {

            var recordsWithGenre = new List<Record>();

            foreach(var record in records)
            {
                foreach(var genre in record.genres)
                {
                    if (genre.ToLower().Contains(genreToFind.ToLower()) && !recordsWithGenre.Contains(record))
                    {
                        recordsWithGenre.Add(record);
                    }
                }
            }
            return recordsWithGenre;
        }

        public async Task<Record> GetByNameAsync(string name)
        {
            return await _recordCollection.Find(_ => _.name == name).FirstOrDefaultAsync();
        }

        public async Task CreateNewRecordAsync(Record newRecord)
        {
            await _recordCollection.InsertOneAsync(newRecord);
        }

        public async Task UpdateRecordAsync(Record recordToUpdate)
        {
            await _recordCollection.ReplaceOneAsync(x => x._id == recordToUpdate._id, recordToUpdate);
        }

        public async Task DeleteRecordAsync(string id)
        {
            await _recordCollection.DeleteOneAsync(x => x._id == id);
        }
    }
}
