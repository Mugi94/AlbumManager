using BusinessObjects.Enum;
using DataAccessLayer.Repository;
using Moq;
using Services.Services;

using BORecord = BusinessObjects.Entity.Record;

namespace Services.Test
{
    public class RecordServiceTest
    {
        private readonly Mock<IGenericRepository<BORecord>> _mockRecordRepository;
        private readonly RecordManager _recordManager;

        public RecordServiceTest()
        {
            _mockRecordRepository = new Mock<IGenericRepository<BORecord>>();
            _recordManager = new RecordManager(_mockRecordRepository.Object);
        }

        [Fact]
        public void GetRecords_ShouldReturnRecords_WhenExists()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAll()).Returns(records);
            var res = _recordManager.GetRecords();

            Assert.NotEmpty(res);
            Assert.Contains(records[0], res);
            Assert.Contains(records[1], res);
        }

        [Fact]
        public void GetRecords_ShouldReturnEmpty_WhenNoRecordsExist()
        {
            _mockRecordRepository.Setup(r => r.GetAll()).Returns([]);
            var res = _recordManager.GetRecords();

            Assert.Empty(res);
        }

        [Fact]
        public void GetRecords_ShouldReturnRecordsOfOneType_WhenTypeIsSpecified()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAll()).Returns(records);
            var res = _recordManager.GetRecords(TypeRecord.EP);

            Assert.NotEmpty(res);
            Assert.Single(res);
            Assert.Equal(TypeRecord.EP, res.First().Type);
        }

        [Fact]
        public void GetRecords_ShouldReturnEmptyOfOneType_WhenNoRecordsExist()
        {
            _mockRecordRepository.Setup(r => r.GetAll()).Returns([]);
            var res = _recordManager.GetRecords(TypeRecord.Single);

            Assert.Empty(res);
        }

        [Fact]
        public void FindRecord_ShouldReturnRecord_WhenExists()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);

            _mockRecordRepository.Setup(r => r.Get(1)).Returns(record);
            var res = _recordManager.FindRecord(1);

            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Record", res.Title);
            Assert.Equal(TypeRecord.Single, res.Type);
        }

        [Fact]
        public void FindRecord_ShouldReturnNull_WhenNotFound()
        {
            _mockRecordRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(value: null);
            var res = _recordManager.FindRecord(1);

            Assert.Null(res);
        }
    }
}