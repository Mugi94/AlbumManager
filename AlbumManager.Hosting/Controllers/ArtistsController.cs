using AutoMapper;
using BusinessObjects.Entity;
using BusinessObjects.DataTransferObject;
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
        public async Task<IActionResult> GetArtists([FromQuery] int? year)
        {
            IEnumerable<Artist> artists;

            if (year.HasValue)
                artists = await _artistManager.GetArtistsAsync(year.Value);
            else
                artists = await _artistManager.GetArtistsAsync();

            return Ok(artists.Select(a => _mapper.Map<ArtistDto>(a)));
        }

        // GET : artists/{id}
        [HttpGet("{id}")]
        public async Task<IActionResult> GetArtist(int id)
        {
            var artist = await _artistManager.FindArtistAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtistDto>(artist));
        }

        // POST : artists
        [HttpPost()]
        public async Task<IActionResult> AddArtist([FromBody] ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            var newArtist = await _artistManager.AddArtistAsync(artist);

            if (newArtist == null)
                return BadRequest($"{artist.Name} already exists");

            return CreatedAtAction(nameof(GetArtist), new { id = newArtist.Id }, _mapper.Map<ArtistDto>(newArtist));
        }

        // PUT : artists/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateArtist(int id, ArtistDto artistDto)
        {
            var artist = await _artistManager.UpdateArtistAsync(id, _mapper.Map<Artist>(artistDto));
            if (artist == null)
                return NotFound();

            return Ok(_mapper.Map<ArtistDto>(artist));
        }

        // DELETE : artists/{id}
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteArtist(int id)
        {
            var artist = await _artistManager.DeleteArtistAsync(id);
            if (artist == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ArtistDto>(artist));
        }
    }
}