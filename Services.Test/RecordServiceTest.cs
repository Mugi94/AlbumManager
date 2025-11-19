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
        public async Task GetRecords_WhenRecordsExist_ShouldReturnList()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(records);
            var res = await _recordManager.GetRecordsAsync();

            Assert.NotEmpty(res);
            Assert.All(records, r => Assert.Contains(r, res));
        }

        [Fact]
        public async Task GetRecords_WhenNoRecordsExist_ShouldReturnEmptyList()
        {
            _mockRecordRepository.Setup(r => r.GetAllAsync()).ReturnsAsync([]);
            var res = await _recordManager.GetRecordsAsync();

            Assert.Empty(res);
        }

        [Fact]
        public async Task GetRecords_WhenTypeIsSpecified_ShouldReturnList()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(records);
            var res = await _recordManager.GetRecordsAsync(TypeRecord.EP);

            Assert.NotEmpty(res);
            Assert.Single(res);
            Assert.Equal(TypeRecord.EP, res.First().Type);
        }

        [Fact]
        public async Task GetRecords_WhenNoRecordsOfOneType_ShouldReturnEmptyList()
        {
            var records = new List<BORecord> {
                new(1, "Record1", new DateTime(2000, 12, 31), TypeRecord.Album, [], []),
                new(2, "Record2", new DateTime(2025, 3, 20), TypeRecord.EP, [], [])
            };

            _mockRecordRepository.Setup(r => r.GetAllAsync()).ReturnsAsync(records);
            var res = await _recordManager.GetRecordsAsync(TypeRecord.Single);

            Assert.Empty(res);
        }

        [Fact]
        public async Task FindRecord_WhenRecordExists_ShouldReturnRecord()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);

            _mockRecordRepository.Setup(r => r.GetAsync(1)).ReturnsAsync(record);
            var res = await _recordManager.FindRecordAsync(1);

            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Record", res.Title);
            Assert.Equal(TypeRecord.Single, res.Type);
        }

        [Fact]
        public async Task FindRecord_WhenRecordNotFound_ShouldReturnNull()
        {
            _mockRecordRepository.Setup(r => r.GetAsync(It.IsAny<int>())).ReturnsAsync(value: null);
            var res = await _recordManager.FindRecordAsync(1);

            Assert.Null(res);
        }

        [Fact]
        public async Task AddRecord_WhenRecordGiven_ShouldAddRecord()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);
            _mockRecordRepository.Setup(r => r.AddAsync(record)).ReturnsAsync(record);

            var res = await _recordManager.AddRecordAsync(record);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Record", res.Title);
            Assert.Equal(TypeRecord.Single, res.Type);
        }

        [Fact]
        public async Task AddRecord_WhenRecordAlreadyExists_ShouldReturnNull()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);
            _mockRecordRepository.Setup(r => r.GetAllAsync()).ReturnsAsync([record]);
            _mockRecordRepository.Setup(r => r.AddAsync(record)).ReturnsAsync((BORecord r) => r);

            var res = await _recordManager.AddRecordAsync(record);
            Assert.Null(res);
        }

        [Fact]
        public async Task DeleteRecord_WhenRecordExists_ShouldRemoveRecord()
        {
            var record = new BORecord(1, "Record", new DateTime(2025, 3, 20), TypeRecord.Single, [], []);
            _mockRecordRepository.Setup(r => r.DeleteAsync(1)).ReturnsAsync(record);

            var res = await _recordManager.DeleteRecordAsync(1);
            Assert.NotNull(res);
            Assert.Equal(1, res.Id);
            Assert.Equal("Record", res.Title);
            Assert.Equal(TypeRecord.Single, res.Type);
        }

        [Fact]
        public async Task DeleteRecord_WhenRecordNotFound_ShouldReturnNull()
        {
            _mockRecordRepository.Setup(r => r.DeleteAsync(It.IsAny<int>())).ReturnsAsync(value: null);

            var res = await _recordManager.DeleteRecordAsync(1);
            Assert.Null(res);
        }
    }
}