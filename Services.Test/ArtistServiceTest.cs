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
        public async Task GetArtists_WhenArtistExists_ShouldReturnList()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2025, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(artists);
            var res = await _artistManager.GetArtistsAsync();

            Assert.NotEmpty(res);
            Assert.All(artists, a => Assert.Contains(a, res));
        }

        [Fact]
        public async Task GetArtists_WhenNoArtistsExist_ShouldReturnEmptyList()
        {
            _mockArtistRepository.Setup(a => a.GetAllAsync()).ReturnsAsync([]);
            var res = await _artistManager.GetArtistsAsync();

            Assert.Empty(res);
        }

        [Fact]
        public async Task GetArtists_WhenDebutYearIsSpecified_ShouldReturnList()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2025, [], []),
                new(3, "Artist3", 2025, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(artists);
            var res = await _artistManager.GetArtistsAsync(2025);

            Assert.NotEmpty(res);
            Assert.Equal(2, res.Count());
            Assert.All(res, a => Assert.Equal(2025, a.DebutYear));
        }

        [Fact]
        public async Task GetArtists_WhenNoArtistsOfOneYear_ShouldReturnEmptyList()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2000, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAllAsync()).ReturnsAsync(artists);
            var res = await _artistManager.GetArtistsAsync(2025);

            Assert.Empty(res);
        }

        [Fact]
        public async Task FindArtist_WhenArtistExists_ShouldReturnArtist()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);

            _mockArtistRepository.Setup(a => a.GetAsync(1)).ReturnsAsync(artist);
            var res = await _artistManager.FindArtistAsync(1);

            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Artist", res.Name);
            Assert.Equal(2025, res.DebutYear);
        }

        [Fact]
        public async Task FindArtist_WhenArtistNotFound_ShouldReturnNull()
        {
            _mockArtistRepository.Setup(a => a.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
            var res = await _artistManager.FindArtistAsync(1);

            Assert.Null(res);
        }

        [Fact]
        public async Task AddArtist_WhenArtistGiven_ShouldAddArtist()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);
            _mockArtistRepository.Setup(a => a.AddAsync(artist)).ReturnsAsync(artist);

            var res = await _artistManager.AddArtistAsync(artist);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Artist", res.Name);
            Assert.Equal(2025, res.DebutYear);
        }

        [Fact]
        public async Task AddArtist_WhenArtistAlreadyExists_ShouldReturnNull()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);
            _mockArtistRepository.Setup(a => a.GetAllAsync()).ReturnsAsync([artist]);
            _mockArtistRepository.Setup(a => a.AddAsync(artist)).ReturnsAsync((Artist a) => a);

            var res = await _artistManager.AddArtistAsync(artist);
            Assert.Null(res);
        }

        [Fact]
        public async Task DeleteArtist_WhenArtistExists_ShouldRemoveArtist()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);
            _mockArtistRepository.Setup(a => a.DeleteAsync(1)).ReturnsAsync(artist);

            var res = await _artistManager.DeleteArtistAsync(1);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Artist", res.Name);
            Assert.Equal(2025, res.DebutYear);
        }

        [Fact]
        public async Task DeleteArtist_WhenArtistNotFound_ShouldReturnNull()
        {
            _mockArtistRepository.Setup(a => a.DeleteAsync(It.IsAny<int>())).ReturnsAsync(value: null);
            var res = await _artistManager.DeleteArtistAsync(1);

            Assert.Null(res);
        }
    }
}