using BusinessObjects.Entity;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace AlbumManager.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistManager _artistManager;

        public ArtistsController(IArtistManager artistManager)
        {
            _artistManager = artistManager;
        }

        // GET : artists
        [HttpGet]
        public IActionResult GetArtists()
        {
            var artists = _artistManager.GetArtists();
            return Ok(artists);
        }

        // GET : artists/{id}
        [HttpGet("{id}")]
        public IActionResult GetArtist(int id)
        {
            var artist = _artistManager.FindArtist(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }

        // GET : artists/year/{year}
        [HttpGet("year/{year}")]
        public IActionResult GetArtistsByYear(int year)
        {
            var artists = _artistManager.GetArtists(year);
            if (artists == null)
            {
                return NotFound();
            }

            return Ok(artists);
        }

        // POST : artists/add
        [HttpPost("add")]
        public IActionResult AddArtist([FromBody] Artist artist)
        {
            var newArtist = _artistManager.AddArtist(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = newArtist.Id}, newArtist);
        }

        // POST : artists/delete
        [HttpPost("delete")]
        public IActionResult DeleteArtist([FromBody] int id)
        {
            var artist = _artistManager.DeleteArtist(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(artist);
        }
    }
}