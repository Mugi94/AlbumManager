using AutoMapper;
using BusinessObjects.Entity;
using BusinessObjects.Enum;
using DataAccessLayer.DataTransferObject;
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
        public IActionResult GetRecords()
        {
            var records = _recordManager.GetRecords();
            return Ok(records.Select(record => _mapper.Map<RecordDto>(record)));
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

        // GET : records/type/{type}
        [HttpGet("type/{type}")]
        public IActionResult GetRecordsByType(TypeRecord type)
        {
            var records = _recordManager.GetRecords(type);
            if (records == null)
            {
                return NotFound();
            }

            return Ok(records.Select(record => _mapper.Map<RecordDto>(record)));
        }

        // POST : records/add
        [HttpPost("add")]
        public IActionResult AddRecord([FromBody] RecordDto recordDto)
        {
            var record = _mapper.Map<Record>(recordDto);
            var newRecord = _recordManager.AddRecord(record);
            return CreatedAtAction(nameof(GetRecord), new { id = newRecord.Id }, _mapper.Map<RecordDto>(newRecord));
        }

        // POST : records/delete
        [HttpPost("delete")]
        public IActionResult DeleteRecord([FromBody] int id)
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