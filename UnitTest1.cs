using FluentAssertions;
using Moq;
using RecordShop.Services;
using RecordShop.Store;
using Record = RecordShop.Models.Record;

namespace RecordShopTests
{
    public class UnitTest1
    {
        [Fact]
        public async Task Test1()
        {
            // arrange
            var records = new List<Record> {
            new Record()
            {
                name = "test1",
                isAvailable = true,
            },
            new Record()
            {
                name = "test1",
                isAvailable = true,
            },
            new Record()
            {
                name = "test1",
                isAvailable = false,
            },
            new Record()
            {
                name = "test2",
                isAvailable = true,
            },

        };

            var mockRepository = new Mock<IRecordRepository>();
            mockRepository.Setup(
                mr => mr.GetAllAsync()).ReturnsAsync(records)
                .Verifiable();
            
            // act

            var filter = new RecordFilterService(mockRepository.Object);

            var result =  await filter.RecordNameCount("test1");

            // assert
            result.Should().Be(2);
            mockRepository.Verify();
        }
    }
}