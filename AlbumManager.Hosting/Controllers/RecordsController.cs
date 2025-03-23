using BusinessObjects.Entity;
using BusinessObjects.Enum;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace AlbumManager.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordManager _recordManager;

        public RecordsController(IRecordManager recordManager)
        {
            _recordManager = recordManager;
        }

        // GET : records
        [HttpGet]
        public IActionResult GetRecords()
        {
            var records = _recordManager.GetRecords();
            return Ok(records);
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

            return Ok(record);
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

            return Ok(records);
        }

        // POST : records/add
        [HttpPost("add")]
        public IActionResult AddRecord([FromBody] Record record)
        {
            var newRecord = _recordManager.AddRecord(record);
            return CreatedAtAction(nameof(GetRecord), new { id = newRecord.Id }, newRecord);
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

            return Ok(record);
        }
    }
}