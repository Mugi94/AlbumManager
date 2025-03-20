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

        [Fact]
        public void GetArtists_ShouldReturnEmpty_WhenNoArtistsExist()
        {
            _mockArtistRepository.Setup(a => a.GetAll()).Returns([]);
            var res = _artistManager.GetArtists();

            Assert.Empty(res);
        }

        [Fact]
        public void GetArtists_ShouldReturnArtistsOfOneYear_WhenDebutYearIsSpecified()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2025, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAll()).Returns(artists);
            var res = _artistManager.GetArtists(2025);

            Assert.NotEmpty(res);
            Assert.Single(res);
            Assert.Equal(2025, res.First().DebutYear);
        }

        [Fact]
        public void GetArtists_ShouldReturnEmptyOfOneYear_WhenNoArtistsExists()
        {
            _mockArtistRepository.Setup(a => a.GetAll()).Returns([]);
            var res = _artistManager.GetArtists(2025);

            Assert.Empty(res);
        }

        [Fact]
        public void FindArtist_ShouldReturnArtist_WhenExists()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);

            _mockArtistRepository.Setup(a => a.Get(1)).Returns(artist);
            var res = _artistManager.FindArtist(1);
            
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Artist", res.Name);
            Assert.Equal(2025, res.DebutYear);
        }

        [Fact]
        public void FindArtist_ShouldReturnNull_WhenNotFound()
        {
            _mockArtistRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(value: null);
            var res = _artistManager.FindArtist(1);

            Assert.Null(res);
        }
    }
}