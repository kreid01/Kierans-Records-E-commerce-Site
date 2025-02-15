﻿using DnsClient.Protocol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Services;
using RecordShop.Store;
using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("/")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IRecordFilterService _filterService;
        public RecordController(IRecordRepository recordRepository, IRecordFilterService recordFilter)
        {
            _recordRepository = recordRepository;
            _filterService = recordFilter;
        }

        [HttpGet]
        [Route("records")]
        public async Task<IActionResult> Get([FromQuery] RecordParameters recordParameters)
        {
            var record = await _recordRepository.GetAllPagedAsync(recordParameters);
            return Ok(record);
        }
        
        [HttpGet]
        [Route("records/all")]
        public async Task<IActionResult> Get()
        {
            var records = await _recordRepository.GetAllAsync();

            //await _filterService.AddStockId();
            //await _filterService.AddFormat();
            foreach (var record in records)
            {
                record.quantity = await _filterService.RecordNameCount(record.name);
            }
            return Ok(records);
        }

    
        [HttpGet]
        [Route("record/{stockNumber}")]
        public async Task<IActionResult> Get(string stockNumber)
        {
            var record = await _recordRepository.GetByStockNumberAsync(stockNumber);
            return Ok(record);
        }

        [HttpGet]
        [Route("records/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var record = await _recordRepository.GetByNameAsync(name);

            record.quantity = await _filterService.RecordNameCount(name);

            return Ok(record);
        }

        [HttpGet]
        [Route("records/sort")]
        public async Task<IActionResult> GetRecordsSorted(string method, [FromQuery] RecordParameters recordParameters)
        {
            var records = await _recordRepository.GetAllAsync();

            var sortedRecords = await _filterService.SortRecords(method, records);

            var uniqueSortedRecords = await _filterService.RemoveDuplicateRecords(sortedRecords);

            return Ok(uniqueSortedRecords.Skip((recordParameters.PageNumber - 1) * recordParameters.PageSize).Take(recordParameters.PageSize).ToList());
        }
        
        [HttpGet]
        [Route("records/genres")]
        public async Task<IActionResult> GetByGenres([FromQuery] RecordParameters recordParameters, string genre)
        {
            var records = await _recordRepository.GetAllAsync();

            var filteredRecords = await _recordRepository.GetByGenre(genre, records);

            var uniqueRecords = await _filterService.RemoveDuplicateRecords(filteredRecords);

            return Ok(uniqueRecords.Skip((recordParameters.PageNumber - 1) * recordParameters.PageSize).Take(recordParameters.PageSize).ToList());

        }

        [HttpGet]
        [Route("records/genres/sorted")]
        public async Task<IActionResult> GetByGenresAndSort([FromQuery] RecordParameters recordParameters, string genre, string method)

        {
            var records = await _recordRepository.GetAllAsync();

           var sortedRecords = await _filterService.SortRecords(method, records);

            var filteredRecordsAndSorteRecords = await _recordRepository.GetByGenre(genre, sortedRecords);

            var uniqueRecords = await _filterService.RemoveDuplicateRecords(filteredRecordsAndSorteRecords);
                        
            return Ok(uniqueRecords.Skip((recordParameters.PageNumber - 1) * recordParameters.PageSize).Take(recordParameters.PageSize).ToList());

        }

        [HttpPost]
        [Route("records")]
        public async Task<IActionResult> Post(Record newRecord)
        {

            var format = newRecord.format;

            var count = await _filterService.RecordFormatCount(format) + 1;

            newRecord.stockNumber = $"{newRecord.format}{count}";

            await _recordRepository.CreateNewRecordAsync(newRecord);       

            return CreatedAtAction(nameof(Get), new { id = newRecord._id }, newRecord);
        }

        [HttpPut]
        [Route("records")]
        public async Task<IActionResult> Put(Record updateRecord)
        {
            Record record = await _recordRepository.GetByStockNumberAsync(updateRecord.stockNumber);
            if (record == null)
            {
                return NotFound();
            }

            await _recordRepository.UpdateRecordAsync(updateRecord);

            return NoContent();
        }
           
        [HttpDelete]
        [Route("records")]
        public async Task<IActionResult> Delete(string id)
        {
            var record = await _recordRepository.GetByStockNumberAsync(id);
            if (record ==null)
            {
                return NotFound();
            }

            await _recordRepository.DeleteRecordAsync(id);

            return NoContent();
        }
  
    }
}
