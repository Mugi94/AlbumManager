using BusinessObjects.Entity;
using DataAccessLayer.Repository;
using Moq;
using Services.Services;

namespace Services.Test
{
    public class TrackServiceTest
    {
        private readonly Mock<IGenericRepository<Track>> _mockTrackRepository;
        private readonly TrackManager _trackManager;

        public TrackServiceTest()
        {
            _mockTrackRepository = new Mock<IGenericRepository<Track>>();
            _trackManager = new TrackManager(_mockTrackRepository.Object);
        }

        [Fact]
        public async Task GetTracks_WhenTrackExists_ShouldReturnList()
        {
            var tracks = new List<Track> {
                new(1, "Track1", 320, [], []),
                new(2, "Track2", 90, [], [])
            };

            _mockTrackRepository.Setup(t => t.GetAllAsync()).ReturnsAsync(tracks);
            var res = await _trackManager.GetTracksAsync();

            Assert.NotEmpty(res);
            Assert.All(tracks, t => Assert.Contains(t, res));
        }

        [Fact]
        public async Task GetTracks_WhenNoTracksExist_ShouldReturnEmptyList()
        {
            _mockTrackRepository.Setup(t => t.GetAllAsync()).ReturnsAsync([]);
            var res = await _trackManager.GetTracksAsync();

            Assert.Empty(res);
        }

        [Fact]
        public async Task GetTracks_WhenArtistIsSpecified_ShouldReturnList()
        {
            var artist1 = new Artist(1, "Artist1", 2000, [], []);
            var artist2 = new Artist(2, "Artist2", 2010, [], []);

            var tracks = new List<Track> {
                new(1, "Track1", 320, [artist1], []),
                new(2, "Track2", 90, [artist2], []),
                new(3, "Track3", 120, [artist1, artist2], [])
            };

            _mockTrackRepository.Setup(t => t.GetAllAsync()).ReturnsAsync(tracks);
            var res = await _trackManager.GetTracksAsync(artist1);

            Assert.NotEmpty(res);
            Assert.Equal(2, res.Count());
            Assert.All(res, t => Assert.Contains(artist1, t.Artists));
        }

        [Fact]
        public async Task GetTracks_WhenNoTracksOfOneArtist_ShouldReturnEmptyList()
        {
            _mockTrackRepository.Setup(t => t.GetAllAsync()).ReturnsAsync([]);
            var res = await _trackManager.GetTracksAsync(new Artist(1, "Artist", 2000, [], []));

            Assert.Empty(res);
        }

        [Fact]
        public async Task FindTrack_WhenTrackExists_ShouldReturnTrack()
        {
            var track = new Track(1, "Track", 90, [], []);

            _mockTrackRepository.Setup(t => t.GetAsync(1)).ReturnsAsync(track);
            var res = await _trackManager.FindTrackAsync(1);

            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Track", res.Title);
            Assert.Equal(90, res.Duration);
        }

        [Fact]
        public async Task FindTrack_WhenTrackNotFound_ShouldReturnNull()
        {
            _mockTrackRepository.Setup(t => t.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
            var res = await _trackManager.FindTrackAsync(1);

            Assert.Null(res);
        }

        [Fact]
        public async Task AddTrack_WhenTrackGiven_ShouldAddTrack()
        {
            var track = new Track(1, "Track", 100, [], []);
            _mockTrackRepository.Setup(t => t.AddAsync(track)).ReturnsAsync(track);

            var res = await _trackManager.AddTrackAsync(track);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Track", res.Title);
            Assert.Equal(100, res.Duration);
        }

        [Fact]
        public async Task AddTrack_WhenTrackAlreadyExists_ShouldReturnNull()
        {
            var track = new Track(1, "Track", 100, [], []);
            _mockTrackRepository.Setup(t => t.GetAllAsync()).ReturnsAsync([track]);
            _mockTrackRepository.Setup(t => t.AddAsync(track)).ReturnsAsync((Track t) => t);

            var res = await _trackManager.AddTrackAsync(track);
            Assert.Null(res);
        }

        [Fact]
        public async Task DeleteTrack_WhenTrackExists_ShouldRemoveTrack()
        {
            var track = new Track(1, "Track", 100, [], []);
            _mockTrackRepository.Setup(t => t.DeleteAsync(1)).ReturnsAsync(track);

            var res = await _trackManager.DeleteTrackAsync(1);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Track", res.Title);
            Assert.Equal(100, res.Duration);
        }

        [Fact]
        public async Task DeleteTrack_WhenTrackNotFound_ShouldReturnNull()
        {
            _mockTrackRepository.Setup(t => t.DeleteAsync(It.IsAny<int>())).ReturnsAsync(value: null);
            var res = await _trackManager.DeleteTrackAsync(1);

            Assert.Null(res);
        }
    }
}