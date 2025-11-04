using AutoMapper;
using BusinessObjects.Entity;
using DataAccessLayer.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace AlbumManager.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksController : ControllerBase
    {
        private readonly ITrackManager _trackManager;
        private readonly IMapper _mapper;

        public TracksController(ITrackManager trackManager, IMapper mapper)
        {
            _trackManager = trackManager;
            _mapper = mapper;
        }

        // GET : tracks
        [HttpGet]
        public IActionResult GetTracks()
        {
            var tracks = _trackManager.GetTracks();
            return Ok(tracks.Select(track => _mapper.Map<TrackDto>(track)));
        }

        // GET : tracks/{id}
        [HttpGet("{id}")]
        public IActionResult GetTrack(int id)
        {
            var track = _trackManager.FindTrack(id);
            if (track == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TrackDto>(track));
        }

        // POST : tracks/add
        [HttpPost("add")]
        public IActionResult AddTrack([FromBody] TrackDto trackDto)
        {
            var track = _mapper.Map<Track>(trackDto);
            var newTrack = _trackManager.AddTrack(track);
            return CreatedAtAction(nameof(GetTrack), new { id = newTrack.Id }, _mapper.Map<TrackDto>(newTrack));
        }

        // POST : tracks/delete
        [HttpPost("delete")]
        public IActionResult DeleteTrack([FromBody] int id)
        {
            var track = _trackManager.DeleteTrack(id);
            if (track == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TrackDto>(track));
        }
    }
}