using BusinessObjects.Entity;
using DataAccessLayer.Repository;
using Moq;
using Services.Services;

namespace Services.Test
{
    public class ArtistServiceTest
    {
        private readonly Mock<IGenericRepository<Artist>> _mockArtistRepository;
        private readonly ArtistManager _artistManager;

        public ArtistServiceTest()
        {
            _mockArtistRepository = new Mock<IGenericRepository<Artist>>();
            _artistManager = new ArtistManager(_mockArtistRepository.Object);
        }

        [Fact]
        public void GetArtists_ShouldReturnArtists_WhenExists()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2025, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAll()).Returns(artists);
            var res = _artistManager.GetArtists();

            Assert.NotEmpty(res);
            Assert.Contains(artists[0], res);
            Assert.Contains(artists[1], res);
        }
    }
}