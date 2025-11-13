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
        public void GetArtists_WhenArtistExists_ShouldReturnList()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2025, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAll()).Returns(artists);
            var res = _artistManager.GetArtists();

            Assert.NotEmpty(res);
            Assert.All(artists, a => Assert.Contains(a, res));
        }

        [Fact]
        public void GetArtists_WhenNoArtistsExist_ShouldReturnEmptyList()
        {
            _mockArtistRepository.Setup(a => a.GetAll()).Returns([]);
            var res = _artistManager.GetArtists();

            Assert.Empty(res);
        }

        [Fact]
        public void GetArtists_WhenDebutYearIsSpecified_ShouldReturnList()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2025, [], []),
                new(3, "Artist3", 2025, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAll()).Returns(artists);
            var res = _artistManager.GetArtists(2025);

            Assert.NotEmpty(res);
            Assert.Equal(2, res.Count());
            Assert.All(res, a => Assert.Equal(2025, a.DebutYear));
        }

        [Fact]
        public void GetArtists_WhenNoArtistsOfOneYear_ShouldReturnEmptyList()
        {
            var artists = new List<Artist> {
                new(1, "Artist1", 2000, [], []),
                new(2, "Artist2", 2000, [], [])
            };

            _mockArtistRepository.Setup(a => a.GetAll()).Returns(artists);
            var res = _artistManager.GetArtists(2025);

            Assert.Empty(res);
        }

        [Fact]
        public void FindArtist_WhenArtistExists_ShouldReturnArtist()
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
        public void FindArtist_WhenArtistNotFound_ShouldReturnNull()
        {
            _mockArtistRepository.Setup(a => a.Get(It.IsAny<int>())).Returns(value: null);
            var res = _artistManager.FindArtist(1);

            Assert.Null(res);
        }

        [Fact]
        public void AddArtist_WhenArtistGiven_ShouldAddArtist()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);
            _mockArtistRepository.Setup(a => a.Add(artist)).Returns(artist);

            var res = _artistManager.AddArtist(artist);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Artist", res.Name);
            Assert.Equal(2025, res.DebutYear);
        }

        [Fact]
        public void AddArtist_WhenArtistAlreadyExists_ShouldReturnNull()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);
            _mockArtistRepository.Setup(a => a.GetAll()).Returns([artist]);
            _mockArtistRepository.Setup(a => a.Add(artist)).Returns((Artist a) => a);

            var res = _artistManager.AddArtist(artist);
            Assert.Null(res);
        }

        [Fact]
        public void DeleteArtist_WhenArtistExists_ShouldRemoveArtist()
        {
            var artist = new Artist(1, "Artist", 2025, [], []);
            _mockArtistRepository.Setup(a => a.Delete(1)).Returns(artist);

            var res = _artistManager.DeleteArtist(1);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Artist", res.Name);
            Assert.Equal(2025, res.DebutYear);
        }

        [Fact]
        public void DeleteArtist_WhenArtistNotFound_ShouldReturnNull()
        {
            _mockArtistRepository.Setup(a => a.Delete(It.IsAny<int>())).Returns(value: null);
            var res = _artistManager.DeleteArtist(1);

            Assert.Null(res);
        }
    }
}