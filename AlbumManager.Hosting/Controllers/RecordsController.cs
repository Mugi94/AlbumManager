using AutoMapper;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using BusinessObjects.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace AlbumManager.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordManager _recordManager;
        private readonly IMapper _mapper;

        public RecordsController(IRecordManager recordManager, IMapper mapper)
        {
            _recordManager = recordManager;
            _mapper = mapper;
        }

        // GET : records
        [HttpGet]
        public async Task<IActionResult> GetRecords([FromQuery] TypeRecord? type)
        {
            IEnumerable<Record> records;

            if (type.HasValue)
                records = await _recordManager.GetRecordsAsync(type.Value);
            else
                records = await _recordManager.GetRecordsAsync();

            return Ok(records.Select(r => _mapper.Map<RecordDto>(r)));
        }

        // GET : records/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetRecord(int id)
        {
            var record = await _recordManager.FindRecordAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecordDto>(record));
        }

        // PUT : records/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, RecordDto recordDto)
        {
            var record = await _recordManager.UpdateRecordAsync(id, _mapper.Map<Record>(recordDto));
            if (record == null)
                return NotFound();

            return Ok(_mapper.Map<RecordDto>(record));
        }

        // POST : records
        [HttpPost()]
        public async Task<IActionResult> AddRecord([FromBody] RecordDto recordDto)
        {
            var record = _mapper.Map<Record>(recordDto);
            var newRecord = await _recordManager.AddRecordAsync(record);

            if (newRecord == null)
                return BadRequest($"{record.Title} already exists");

            return CreatedAtAction(nameof(GetRecord), new { id = newRecord.Id }, _mapper.Map<RecordDto>(newRecord));
        }

        // DELETE : records
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteRecord(int id)
        {
            var record = await _recordManager.DeleteRecordAsync(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecordDto>(record));
        }
    }
}