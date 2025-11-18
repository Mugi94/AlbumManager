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
        public IActionResult GetArtists([FromQuery] int? year)
        {
            if (year.HasValue)
                return Ok(_artistManager.GetArtists(year.Value).Select(a => _mapper.Map<ArtistDto>(a)));
            else
                return Ok(_artistManager.GetArtists().Select(a => _mapper.Map<ArtistDto>(a)));
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

        // POST : artists
        [HttpPost()]
        public IActionResult AddArtist([FromBody] ArtistDto artistDto)
        {
            var artist = _mapper.Map<Artist>(artistDto);
            var newArtist = _artistManager.AddArtist(artist);
            
            if (newArtist == null)
                return BadRequest($"{artist.Name} already exists");

            return CreatedAtAction(nameof(GetArtist), new { id = newArtist.Id }, _mapper.Map<ArtistDto>(newArtist));
        }

        // PUT : artists/{id}
        [HttpPut("{id}")]
        public IActionResult UpdateArtist(int id, ArtistDto artistDto)
        {
            var artist = _artistManager.UpdateArtist(id, _mapper.Map<Artist>(artistDto));
            if (artist == null)
                return NotFound();

            return Ok(_mapper.Map<ArtistDto>(artist));
        }

        // DELETE : artists/{id}
        [HttpDelete("{id}")]
        public IActionResult DeleteArtist(int id)
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