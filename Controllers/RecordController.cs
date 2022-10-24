using DnsClient.Protocol;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Services;
using RecordShop.Store;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("/")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _recordRepository;
        private readonly IRecordFilterService _filterService;
        public RecordController(IRecordRepository recordRepository, ICartRepository cartRepository, IRecordFilterService recordFilter)
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

            await _filterService.MigrateData();
            foreach (var record in records)
            {
                record.quantity = await _filterService.RecordNameCount(record.name);
            }
            return Ok(records);
        }

        [HttpGet]
        [Route("record/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var record = await _recordRepository.GetByIdAsync(id);
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

        [HttpPost]
        [Route("records")]
        public async Task<IActionResult> Post(Record newRecord)
        {
            await _recordRepository.CreateNewRecordAsync(newRecord);
            return CreatedAtAction(nameof(Get), new { id = newRecord._id }, newRecord);
        }

        [HttpPut]
        [Route("records")]
        public async Task<IActionResult> Put(Record updateRecord)
        {
            Record record = await _recordRepository.GetByIdAsync(updateRecord._id);
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
            var record = await _recordRepository.GetByIdAsync(id);
            if (record ==null)
            {
                return NotFound();
            }

            await _recordRepository.DeleteRecordAsync(id);

            return NoContent();
        }
  
    }
}
