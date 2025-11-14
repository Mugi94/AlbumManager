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
        public IActionResult GetRecords([FromQuery] TypeRecord? type)
        {
            if (type.HasValue)
                return Ok(_recordManager.GetRecords(type.Value).Select(r => _mapper.Map<RecordDto>(r)));
            else
                return Ok(_recordManager.GetRecords().Select(r => _mapper.Map<RecordDto>(r)));
        }

        // GET : records/{id}
        [HttpGet("{id}")]
        public IActionResult GetRecord(int id)
        {
            var record = _recordManager.FindRecord(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecordDto>(record));
        }

        // POST : records
        [HttpPost()]
        public IActionResult AddRecord([FromBody] RecordDto recordDto)
        {
            var record = _mapper.Map<Record>(recordDto);
            var newRecord = _recordManager.AddRecord(record);
            
            if (newRecord == null)
                return BadRequest($"{record.Title} already exists");

            return CreatedAtAction(nameof(GetRecord), new { id = newRecord.Id }, _mapper.Map<RecordDto>(newRecord));
        }

        // DELETE : records
        [HttpDelete("{id}")]
        public IActionResult DeleteRecord(int id)
        {
            var record = _recordManager.DeleteRecord(id);
            if (record == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RecordDto>(record));
        }
    }
}