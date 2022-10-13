using Microsoft.AspNetCore.Mvc;
using RecordShop.Models;
using RecordShop.Store;

namespace RecordShop.Controllers
{
    [ApiController]
    [Route("/")]
    public class RecordController : ControllerBase
    {
        private readonly IRecordRepository _irecordRepository;
        public RecordController(IRecordRepository irecordRepository)
        {
            _irecordRepository = irecordRepository;
        }

        [HttpGet]
        [Route("records")]
        public async Task<IActionResult> Get([FromQuery] RecordParameters recordParameters)
        {
            var record = await _irecordRepository.GetAllAsync(recordParameters);
            return Ok(record);
        }
        [HttpGet]
        [Route("records/all")]
        public async Task<IActionResult> Get()
        {
            var record = await _irecordRepository.GetAllByAsync();
            return Ok(record);
        }

        [HttpGet]
        [Route("record/{id}")]
        public async Task<IActionResult> Get(string id)
        {
            var record = await _irecordRepository.GetByIdAsync(id);
            return Ok(record);
        }

        [HttpGet]
        [Route("records/{name}")]
        public async Task<IActionResult> GetByName(string name)
        {
            var record = await _irecordRepository.GetByNameAsync(name);
            return Ok(record);
        }

        [HttpPost]
        [Route("records")]
        public async Task<IActionResult> Post(Record newRecord)
        {
            await _irecordRepository.CreateNewRecordAsync(newRecord);
            return CreatedAtAction(nameof(Get), new { id = newRecord._id }, newRecord);
        }

        [HttpPut]
        [Route("records")]
        public async Task<IActionResult> Put(Record updateRecord)
        {
            Record record = await _irecordRepository.GetByIdAsync(updateRecord._id);
            if (record == null)
            {
                return NotFound();
            }

            await _irecordRepository.UpdateRecordAsync(updateRecord);

            return NoContent();
        }

        [HttpDelete]
        [Route("records")]
        public async Task<IActionResult> Delete(string id)
        {
            var record = await _irecordRepository.GetByIdAsync(id);
            if (record ==null)
            {
                return NotFound();
            }

            await _irecordRepository.DeleteRecordAsync(id);

            return NoContent();
        }
    }
}
