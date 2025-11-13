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
        public void GetRecords_WhenRecordsExist_ShouldReturnList()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAll()).Returns(records);
            var res = _recordManager.GetRecords();

            Assert.NotEmpty(res);
            Assert.All(records, r => Assert.Contains(r, res));
        }

        [Fact]
        public void GetRecords_WhenNoRecordsExist_ShouldReturnEmptyList()
        {
            _mockRecordRepository.Setup(r => r.GetAll()).Returns([]);
            var res = _recordManager.GetRecords();

            Assert.Empty(res);
        }

        [Fact]
        public void GetRecords_WhenTypeIsSpecified_ShouldReturnList()
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
        public void GetRecords_WhenNoRecordsOfOneType_ShouldReturnEmptyList()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAll()).Returns(records);
            var res = _recordManager.GetRecords(TypeRecord.Single);

            Assert.Empty(res);
        }

        [Fact]
        public void FindRecord_WhenRecordExists_ShouldReturnRecord()
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
        public void FindRecord_WhenRecordNotFound_ShouldReturnNull()
        {
            _mockRecordRepository.Setup(r => r.Get(It.IsAny<int>())).Returns(value: null);
            var res = _recordManager.FindRecord(1);

            Assert.Null(res);
        }

        [Fact]
        public void AddRecord_WhenRecordGiven_ShouldAddRecord()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);
            _mockRecordRepository.Setup(r => r.Add(record)).Returns(record);

            var res = _recordManager.AddRecord(record);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Record", res.Title);
            Assert.Equal(TypeRecord.Single, res.Type);
        }

        [Fact]
        public void AddRecord_WhenRecordAlreadyExists_ShouldReturnNull()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);
            _mockRecordRepository.Setup(r => r.GetAll()).Returns([record]);
            _mockRecordRepository.Setup(r => r.Add(record)).Returns((BORecord r) => r);

            var res = _recordManager.AddRecord(record);
            Assert.Null(res);
        }

        [Fact]
        public void DeleteRecord_WhenRecordExists_ShouldRemoveRecord()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);
            _mockRecordRepository.Setup(r => r.Delete(1)).Returns(record);
            
            var res = _recordManager.DeleteRecord(1);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Record", res.Title);
            Assert.Equal(TypeRecord.Single, res.Type);
        }

        [Fact]
        public void DeleteRecord_WhenRecordNotFound_ShouldReturnNull()
        {
            _mockRecordRepository.Setup(r => r.Delete(It.IsAny<int>())).Returns(value: null);
            
            var res = _recordManager.DeleteRecord(1);
            Assert.Null(res);
        }
    }
}