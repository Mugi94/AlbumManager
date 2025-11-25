using AutoMapper;
using BusinessObjects.Entity;
using BusinessObjects.DataTransferObject;
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
        public async Task<IActionResult> GetTracks()
        {
            var tracks = await _trackManager.GetTracksAsync();
            return Ok(tracks.Select(track => _mapper.Map<TrackResponse>(track)));
        }

        // GET : tracks/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetTrack(int id)
        {
            var track = await _trackManager.FindTrackAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TrackResponse>(track));
        }

        // POST : tracks
        [HttpPost()]
        public async Task<IActionResult> AddTrack([FromBody] TrackRequest trackDto)
        {
            var track = _mapper.Map<Track>(trackDto);
            var newTrack = await _trackManager.AddTrackAsync(track);

            if (newTrack == null)
                return BadRequest($"{track.Title} already exists");

            return CreatedAtAction(nameof(GetTrack), new { id = newTrack.Id }, _mapper.Map<TrackResponse>(newTrack));
        }

        // PUT : tracks/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateRecord(int id, TrackRequest trackDto)
        {
            var track = await _trackManager.UpdateTrackAsync(id, _mapper.Map<Track>(trackDto));
            if (track == null)
                return NotFound();

            return Ok(_mapper.Map<TrackResponse>(track));
        }

        // DELETE : tracks
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTrack(int id)
        {
            var track = await _trackManager.DeleteTrackAsync(id);
            if (track == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<TrackResponse>(track));
        }
    }
}