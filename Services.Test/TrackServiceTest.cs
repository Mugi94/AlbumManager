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
        public void GetTracks_WhenTrackExists_ShouldReturnList()
        {
            var tracks = new List<Track> {
                new(1, "Track1", 320, [], []),
                new(2, "Track2", 90, [], [])
            };

            _mockTrackRepository.Setup(t => t.GetAll()).Returns(tracks);
            var res = _trackManager.GetTracks();

            Assert.NotEmpty(res);
            Assert.All(tracks, t => Assert.Contains(t, res));
        }

        [Fact]
        public void GetTracks_WhenNoTracksExist_ShouldReturnEmptyList()
        {
            _mockTrackRepository.Setup(t => t.GetAll()).Returns([]);
            var res = _trackManager.GetTracks();

            Assert.Empty(res);
        }

        [Fact]
        public void GetTracks_WhenArtistIsSpecified_ShouldReturnList()
        {
            var artist1 = new Artist(1, "Artist1", 2000, [], []);
            var artist2 = new Artist(2, "Artist2", 2010, [], []);

            var tracks = new List<Track> {
                new(1, "Track1", 320, [artist1], []),
                new(2, "Track2", 90, [artist2], []),
                new(3, "Track3", 120, [artist1, artist2], [])
            };

            _mockTrackRepository.Setup(t => t.GetAll()).Returns(tracks);
            var res = _trackManager.GetTracks(artist1);

            Assert.NotEmpty(res);
            Assert.Equal(2, res.Count());
            Assert.All(res, t => Assert.Contains(artist1, t.Artists));
        }

        [Fact]
        public void GetTracks_WhenNoTracksOfOneArtist_ShouldReturnEmptyList()
        {
            _mockTrackRepository.Setup(t => t.GetAll()).Returns([]);
            var res = _trackManager.GetTracks(new Artist(1, "Artist", 2000, [], []));

            Assert.Empty(res);
        }

        [Fact]
        public void FindTrack_WhenTrackExists_ShouldReturnTrack()
        {
            var track = new Track(1, "Track", 90, [], []);

            _mockTrackRepository.Setup(t => t.Get(1)).Returns(track);
            var res = _trackManager.FindTrack(1);

            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Track", res.Title);
            Assert.Equal(90, res.Duration);
        }

        [Fact]
        public void FindTrack_WhenTrackNotFound_ShouldReturnNull()
        {
            _mockTrackRepository.Setup(t => t.Get(It.IsAny<int>())).Returns(value: null);
            var res = _trackManager.FindTrack(1);

            Assert.Null(res);
        }

        [Fact]
        public void AddTrack_WhenTrackGiven_ShouldAddTrack()
        {
            var track = new Track(1, "Track", 100, [], []);
            _mockTrackRepository.Setup(t => t.Add(track)).Returns(track);

            var res = _trackManager.AddTrack(track);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Track", res.Title);
            Assert.Equal(100, res.Duration);
        }

        [Fact]
        public void AddTrack_WhenTrackAlreadyExists_ShouldReturnNull()
        {
            var track = new Track(1, "Track", 100, [], []);
            _mockTrackRepository.Setup(t => t.GetAll()).Returns([track]);
            _mockTrackRepository.Setup(t => t.Add(track)).Returns((Track t) => t);

            var res = _trackManager.AddTrack(track);
            Assert.Null(res);
        }

        [Fact]
        public void DeleteTrack_WhenTrackExists_ShouldRemoveTrack()
        {
            var track = new Track(1, "Track", 100, [], []);
            _mockTrackRepository.Setup(t => t.Delete(1)).Returns(track);

            var res = _trackManager.DeleteTrack(1);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Track", res.Title);
            Assert.Equal(100, res.Duration);
        }

        [Fact]
        public void DeleteTrack_WhenTrackNotFound_ShouldReturnNull()
        {
            _mockTrackRepository.Setup(t => t.Delete(It.IsAny<int>())).Returns(value: null);
            var res = _trackManager.DeleteTrack(1);

            Assert.Null(res);
        }
    }
}