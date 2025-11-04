using AutoMapper;
using BusinessObjects.Entity;
using DataAccessLayer.DataTransferObject;
using Microsoft.AspNetCore.Mvc;
using Services.Services;

namespace AlbumManager.Hosting.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArtistsController : ControllerBase
    {
        private readonly IArtistManager _artistManager;
        private readonly IMapper _mapper;

        public ArtistsController(IArtistManager artistManager, IMapper mapper)
        {
            _artistManager = artistManager;
            _mapper = mapper;
        }

        // GET : artists
        [HttpGet]
        public IActionResult GetArtists()
        {
            var artists = _artistManager.GetArtists();
            return Ok(artists.Select(artist => _mapper.Map<ArtistDto>(artist)));
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

            return Ok(_mapper.Map<ArtistDto>(artist));
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

            return Ok(artists.Select(artist => _mapper.Map<ArtistDto>(artist)));
        }

        // POST : artists/add
        [HttpPost("add")]
        public IActionResult AddArtist([FromBody] ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            var newArtist = _artistManager.AddArtist(artist);
            return CreatedAtAction(nameof(GetArtist), new { id = newArtist.Id}, _mapper.Map<ArtistDto>(newArtist));
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

            return Ok(_mapper.Map<ArtistDto>(artist));
        }
    }
}