using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Extensions;
using MongoDB.Driver;
using RecordShop.Models;
using RecordShop.Store;
using System.Linq;

namespace RecordShop.Services
{
    public class RecordFilterService : IRecordFilterService
    {
        private readonly IRecordRepository _recordRepository;

        public RecordFilterService(IRecordRepository recordRepository)
        {
            _recordRepository = recordRepository;
        }

        public async Task<int> RecordNameCount(string value)
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
                record.isReservedInCart = false;
                await _recordRepository.UpdateRecordAsync(record);
            }
        }

        public async Task AddStockId()
        {
            var records = await _recordRepository.GetAllAsync();

            int index = 1;
            foreach (var record in records)
            {
                record.stockNumber = $"LP{index}";
                index++;
                await _recordRepository.UpdateRecordAsync(record);
            }


        }

        public async Task AddFormat()
        {
            var records = await _recordRepository.GetAllAsync();

            foreach (var record in records)
            {
                record.format = "LP";
                await _recordRepository.UpdateRecordAsync(record);
            }


        }

        public async Task<int> RecordFormatCount(string value)
        {
            var result = await _recordRepository.GetAllAsync();

            return result.Where(r => r.format == value).Count();

        }

        public async Task<List<Record>> SortRecords(string method, List<Record> records)
        {

            if (method == "Price >")
            {
                records = records.OrderBy(r => r.price).ToList();
            }

            if (method == "Price <")
            {
                records = records.OrderByDescending(r => r.price).ToList();
            }

            if (method == "Release Year >")
            {
                records = records.OrderBy(r => r.releaseYear).ToList();
            }

            if (method == "Release Year <")
            {
                records = records.OrderByDescending(r => r.releaseYear).ToList();
            }

            if (method == "Record Name >")
            {
                records = records.OrderBy(r => r.name).ToList();
            }

            if (method == "Record Name >")
            {
                records = records.OrderByDescending(r => r.name).ToList();
            }

            return records;
        }

        public async Task<List<Record>> RemoveDuplicateRecords(List<Record> records)
        {
            var uniqueRecordList = new List<Record>();

            foreach (var record in records)
            {
                uniqueRecordList.Where(r => r.name != record.name).ToList();
               
            }

            return uniqueRecordList;
        }
    }
}
