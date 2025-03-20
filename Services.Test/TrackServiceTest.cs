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
        public void GetTracks_ShouldReturnTracks_WhenExists()
        {
            var tracks = new List<Track> {
                new(1, "Track1", 320, [], []),
                new(2, "Track2", 90, [], [])
            };

            _mockTrackRepository.Setup(t => t.GetAll()).Returns(tracks);
            var res = _trackManager.GetTracks();

            Assert.NotEmpty(res);
            Assert.Contains(tracks[0], res);
            Assert.Contains(tracks[1], res);
        }

        [Fact]
        public void GetTracks_ShouldReturnEmpty_WhenNoTracksExist()
        {
            _mockTrackRepository.Setup(t => t.GetAll()).Returns([]);
            var res = _trackManager.GetTracks();

            Assert.Empty(res);
        }

        [Fact]
        public void GetTracks_ShouldReturnTracksOfOneArtist_WhenArtistIsSpecified()
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
        public void GetTracks_ShouldReturnEmptyOfOneArtist_WhenNoTracksExist()
        {
            _mockTrackRepository.Setup(t => t.GetAll()).Returns([]);
            var res = _trackManager.GetTracks(new Artist(1, "Artist", 2000, [], []));

            Assert.Empty(res);
        }

        [Fact]
        public void FindTrack_ShouldReturnTrack_WhenExist()
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
        public void FindTrack_ShouldReturnNull_WhenNotFound()
        {
            _mockTrackRepository.Setup(t => t.Get(It.IsAny<int>())).Returns(value: null);
            var res = _trackManager.FindTrack(1);

            Assert.Null(res);
        }
    }
}