﻿using Microsoft.AspNetCore.Components.Web;
using Microsoft.OpenApi.Extensions;
using MongoDB.Driver;
using RecordShop.Models;
using RecordShop.Store;

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
           
    }
}
