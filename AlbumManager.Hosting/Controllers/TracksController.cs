using BusinessObjects.Entity;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace AlbumManager.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TracksManager : ControllerBase
    {
        private readonly ITrackManager _trackManager;

        public TracksManager(ITrackManager trackManager)
        {
            _trackManager = trackManager;
        }

        // GET : tracks
        [HttpGet]
        public IActionResult GetTracks()
        {
            var tracks = _trackManager.GetTracks();
            return Ok(tracks);
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

            return Ok(track);
        }

        // POST : tracks/add
        [HttpPost("add")]
        public IActionResult AddTrack([FromBody] Track track)
        {
            var newTrack = _trackManager.AddTrack(track);
            return CreatedAtAction(nameof(GetTrack), new { id = newTrack.Id }, newTrack);
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

            return Ok(track);
        }
    }
}